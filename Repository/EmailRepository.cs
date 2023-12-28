using SendEmail.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SendEmail.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly Semaphore semaphore = new Semaphore(1, 1);

        private static readonly IList<string> sendTokens = new List<string>();

        public void SendEmailAsync(ConfigurationEmail configurationEmail)
        {
            ThreadEmailAsync(configurationEmail.Email, configurationEmail.Client);
        }

        private Task ThreadEmailAsync(Email email, SmtpClient client)
        {
            SmtpClient smtpClient = client;
            MailAddress from = new MailAddress(email.From, email.Alias, email.TextEncoding);
            smtpClient.SendCompleted += new SendCompletedEventHandler(EmailCallbackAsync);

            foreach (var item in email.To)
            {
                MailAddress to = new MailAddress(item);
                MailMessage mailMessage = new MailMessage(from, to);
                string token = item;

                mailMessage.SubjectEncoding = email.TextEncoding;
                mailMessage.BodyEncoding = email.TextEncoding;
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;

                semaphore.WaitOne();
                smtpClient.SendAsync(mailMessage, token);
            }

            return Task.CompletedTask;
        }

        private void EmailCallbackAsync(object sender, AsyncCompletedEventArgs e)
        {
            string userState = (string)e.UserState;

            if (!sendTokens.Contains(userState))
            {
                semaphore.Release();
                if (e.Cancelled)
                {
                    //ToDo implemantar o log de envio cancelados
                }
                else if (e.Error != null)
                {
                    //Todo implementar os logs de erros
                }
                sendTokens.Add(userState);
            }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SendEmail.Model;

namespace SendEmail.Services
{
    public class EmailAsync : IServiceSendEmail
    {
        /// <summary>
        /// Estrutura temporaria para envio de email
        /// </summary>
        private struct EmailStruct
        {
            public string Alias { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public Encoding TextEncoding { get; set; }
            public bool IsBodyHtml { get; set; }
        }

        //private readonly object monitorObj = new object();

        /// <summary>
        /// Semaforo binario para execução de um envio por vez
        /// </summary>
        private Semaphore semaphore = new Semaphore(1, 1);

        /// <summary>
        /// Contador de token (garante o uso único)
        /// </summary>
        private static IList<string> sendTokens = new List<string>();

        public void SendEmailAsync(ConfigurationEmail configurationEmail)
        {
            foreach (string to in configurationEmail.To)
            {
                ThreadEmailAsync(new EmailStruct()
                {
                    Alias = configurationEmail.Alias,
                    From = configurationEmail.From,
                    To = to,
                    Subject = configurationEmail.Subject,
                    Body = configurationEmail.Body,
                    TextEncoding = configurationEmail.TextEnconding,
                    IsBodyHtml = configurationEmail.IsBodyHtml
                }, configurationEmail.Client);
            }
        }

        public void SendEmailAsync(ConfigurationEmail configurationEmail, Semaphore semaphore)
        {
            this.semaphore = semaphore;
            SendEmailAsync(configurationEmail);
        }

        /// <summary>
        /// Thread para envio de email
        /// </summary>
        /// <param name="emailStruct">EmailStruct</param>
        /// <param name="smtpClient">SmtpClient</param>
        private void ThreadEmailAsync(EmailStruct email, SmtpClient client)
        {
            new Thread(() =>
            {
                SmtpClient smtpClient = client;
                MailAddress from = new MailAddress(email.From, email.Alias, email.TextEncoding);
                MailAddress to = new MailAddress(email.To);
                MailMessage mailMessage = new MailMessage(from, to);
                smtpClient.SendCompleted += new SendCompletedEventHandler(EmailCallbackAsync);

                mailMessage.SubjectEncoding = email.TextEncoding;
                mailMessage.BodyEncoding = email.TextEncoding;
                mailMessage.IsBodyHtml = email.IsBodyHtml;
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;

                string token = email.To;
                semaphore.WaitOne();
                smtpClient.SendAsync(mailMessage, token);

            }).Start();
        }

        /// <summary>
        /// Callback para release do semaforo
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">AsyncCompletedEventArgs</param>
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

using SendEmail.Model;
using SendEmail.Repository;

namespace SendEmail.Services
{
    public class EmailService : IEmailService
    {
        readonly IEmailRepository emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public void SendEmailAsync(ConfigurationEmail configurationEmail) => emailRepository.SendEmailAsync(configurationEmail);
    }
}

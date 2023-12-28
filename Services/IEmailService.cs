using SendEmail.Model;

namespace SendEmail.Services
{
    public interface IEmailService
    {
        void SendEmailAsync(ConfigurationEmail configurationEmail);
    }
}

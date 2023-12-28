using SendEmail.Model;

namespace SendEmail.Repository
{
    public interface IEmailRepository
    {
       void SendEmailAsync(ConfigurationEmail configurationEmail);
    }
}
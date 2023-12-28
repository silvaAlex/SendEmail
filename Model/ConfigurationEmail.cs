using System.Net.Mail;

namespace SendEmail.Model
{
    public class ConfigurationEmail
    {
        public Email Email { get; set; }
        public SmtpClient Client { get; set; }
    }
}
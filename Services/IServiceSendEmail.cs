using System.Threading;
using SendEmail.Model;

namespace SendEmail.Services
{
    public interface IServiceSendEmail
    {
        /// <summary>
        /// Envia email de forma asyncrona
        /// </summary>
        /// <param name="configurationEmail">ConfigurationEmail</param>
        public void SendEmailAsync(ConfigurationEmail configurationEmail);

        /// <summary>
        /// Envia email de forma asyncrona para templates diferentes
        /// </summary>
        /// <param name="configurationEmail">ConfigurationEmail</param>
        /// <param name="semaphore">Semaphore externo para controle de threads</param>
        public void SendEmailAsync(ConfigurationEmail configurationEmail, Semaphore semaphore);
    }
}
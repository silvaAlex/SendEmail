using System.Net.Mail;
using System.Text;

namespace SendEmail.Model
{
    public class ConfigurationEmail
    {
        /// <summary>
        /// Remetente
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Apelido para o remetente
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Array Destinatarios para serem enviados
        /// </summary>
        public string[] To { get; set; }
        /// <summary>
        /// Assunto para menasegem de email
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Corpo da menasegem de email
        /// </summary>
        public string Body { get; set; }
        ///<summary>
        /// Codificação do texto (preferencial UTF-8)
        ///</summary>
        public Encoding TextEnconding { get; set; }
        /// <summary>
        /// Mensagem esta em HTML?
        /// </summary>
        public bool IsBodyHtml { get; set; }
        /// <summary>
        /// Configurações de Smtp do servidor de  email
        /// </summary>
        public SmtpClient Client { get; set; }

        /// <summary>
        /// Clona o objeto para uma nova referencia
        /// </summary>
        /// <returns>ConfigurationEmail</returns>
        public ConfigurationEmail Clone()
        {
            return new ConfigurationEmail()
            {
                From = From,
                Alias = Alias,
                To = To,
                Subject = Subject,
                TextEnconding = TextEnconding,
                IsBodyHtml = IsBodyHtml,
                Client = Client
            };

        }
    }
}
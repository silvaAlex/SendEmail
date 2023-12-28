using System.Text;

namespace SendEmail.Model
{
    public class Email
    {
        public string Alias { get; set; }
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Encoding TextEncoding { get; set; }
        public bool IsBodyHtml { get; set; }

        public Email Clone()
        {
            return new Email()
            {
                From = From,
                Alias = Alias,
                To = To,
                Subject = Subject,
                TextEncoding = TextEncoding,
                IsBodyHtml = IsBodyHtml,
            };
        }
    }
}

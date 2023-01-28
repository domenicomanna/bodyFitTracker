using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace Api.Common
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; private set; }
        public string Subject { get; private set; }
        public string HtmlBody { get; set; }
        public string TextBody { get; set; }

        public EmailMessage(string to, string subject)
            : this(new string[] { to }, subject) { }

        public EmailMessage(IEnumerable<string> to, string subject)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
        }
    }
}

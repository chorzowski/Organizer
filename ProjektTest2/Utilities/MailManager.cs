using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ProjektTest2.Utilities
{
    public class MailManager : IMailManager
    {
        //public String _fromMessageName { get; set; }
        //public String _toMessageName { get; set; }
        //public String _fromMessageMail { get; set; }
        //public String _toMessageMail { get; set; }
        //public String _subjectMessage { get; set; }
        //public String _textMessage { get; set; }

        //public MailManager(String fromMessageName, String toMessageName, String fromMessageMail, String toMessageMail, String subjectMessage, String textMessage)
        //{
        //    fromMessageName = _fromMessageName;
        //    toMessageName = _toMessageName;
        //    fromMessageMail = _fromMessageMail;
        //    toMessageMail = _toMessageMail;
        //    subjectMessage = _subjectMessage;
        //    textMessage = _textMessage;
        //}

        public MimeMessage CreateMessage(String fromMessageName, String toMessageName, String fromMessageMail, String toMessageMail, String subjectMessage, String textMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromMessageName, fromMessageMail));
            message.To.Add(new MailboxAddress(toMessageName, toMessageMail));
            message.Subject = subjectMessage;
            message.Body = new TextPart("plain")
            {
                Text = textMessage
            };
            return message;
        }
    }
}

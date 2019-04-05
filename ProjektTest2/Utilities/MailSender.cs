using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ProjektTest2.Utilities
{
    public class MailSender : IMailSender
    {
        public void SendMessage(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("XXX");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}

using MimeKit;

namespace ProjektTest2.Utilities
{
    public interface IMailSender
    {
        void SendMessage(MimeMessage message);
    }
}
using MimeKit;

namespace ProjektTest2.Utilities
{
    public interface IMailManager
    {
        MimeMessage CreateMessage(string fromMessageName, string toMessageName, string fromMessageMail, string toMessageMail, string subjectMessage, string textMessage);
    }
}
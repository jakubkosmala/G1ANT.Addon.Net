using MailKit.Net.Imap;
using MailKit.Net.Smtp;

namespace G1ANT.Addon.Net.Models
{
    public interface IAuthenticationModel
    {
        void Authenticate(ImapClient client);
        void Authenticate(SmtpClient client);
    }
}

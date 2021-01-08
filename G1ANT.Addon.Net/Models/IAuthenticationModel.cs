using MailKit.Net.Imap;

namespace G1ANT.Addon.Net.Models
{
    public interface IAuthenticationModel
    {
        void Authenticate(ImapClient client);
    }
}

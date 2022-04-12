
namespace G1ANT.Addon.Net.Models
{
    public interface IOauthWizardModel
    {
        void RequestTokenInteractive();
        bool IsImapRequested { get; }
        bool IsSmtpRequested { get; }
        string SmtpHost { get; }
        string SmtpPort { get; }
        string ImapHost { get; }
        string ImapPort { get; }
        string[] RequiredIndexes { get; }
    }
}

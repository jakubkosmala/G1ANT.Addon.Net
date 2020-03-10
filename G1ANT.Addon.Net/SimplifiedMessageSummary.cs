using MailKit;
using MimeKit;
using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    public class SimplifiedMessageSummary : MessageSummary
    {
        public new string TextBody;
        public IMailFolder Folder;
        public ListStructure MessageAttachments;

        public SimplifiedMessageSummary(MessageSummary summary, IMailFolder folder, ListStructure attachmentsList) : base(summary.Index)
        {
            TextBody = ParseBody(summary, folder);
            MessageAttachments = attachmentsList;
            Envelope = summary.Envelope;
            UniqueId = summary.UniqueId;
            Folder = folder;
        }

        private static string ParseBody(IMessageSummary message, IMailFolder folder)
        {
            return message.TextBody != null ? ((TextPart)folder.GetBodyPart(message.UniqueId, message.TextBody)).Text : string.Empty;
        }
    }
}

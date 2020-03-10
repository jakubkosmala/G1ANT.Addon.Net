using System;
using MailKit;
using MimeKit;
using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Structure(Name = "mail", Priority = 10, Default = 0, AutoCreate = false, Tooltip = "This structure stores current information about a mail message, which was downloaded with the `mail.imap` command")]
    public class MailStructure : StructureTyped<SimplifiedMessageSummary>
    {
        private const string IdIndex = "id";
        private const string FromIndex = "from";
        private const string ToIndex = "to";
        private const string CcIndex = "cc";
        private const string BccIndex = "bcc";
        private const string SubjectIndex = "subject";
        private const string BodyIndex = "content";
        private const string HtmlBodyIndex = "htmlcontent";
        private const string DateIndex = "date";
        private const string PriorityIndex = "priority";
        private const string AttachmentsIndex = "attachments";
        private const string IsReply = "isreply";

        private MimeMessage FullMessage;

        public MailStructure() : base(new MessageSummary(0))
        {
        }

        public MailStructure(SimplifiedMessageSummary value, string format = "") :
            base(value, format)
        {
            Init();
        }

        public MailStructure(object value, string format = "", AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(IdIndex);
            Indexes.Add(SubjectIndex);
            Indexes.Add(FromIndex);
            Indexes.Add(ToIndex);
            Indexes.Add(CcIndex);
            Indexes.Add(BccIndex);
            Indexes.Add(DateIndex);
            Indexes.Add(AttachmentsIndex);
            Indexes.Add(PriorityIndex);
            Indexes.Add(BodyIndex);
            Indexes.Add(HtmlBodyIndex);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                return new MailStructure(Value.NormalizedSubject, Format);
            }

            switch (index.ToLower())
            {
                case IdIndex:
                    return new TextStructure(Value.Envelope.MessageId, null, Scripter);
                case SubjectIndex:
                    return new TextStructure(Value.NormalizedSubject, null, Scripter);
                case FromIndex:
                    return new TextStructure(Value.Envelope.From, null, Scripter);
                case ToIndex:
                    return new TextStructure(Value.Envelope.To, null, Scripter);
                case CcIndex:
                    return new TextStructure(Value.Envelope.Cc, null, Scripter);
                case BccIndex:
                    return new TextStructure(Value.Envelope.Bcc, null, Scripter);
                case DateIndex:
                    return new DateTimeStructure(Value.Date, "");
                case IsReply:
                    return new BooleanStructure(Value.IsReply);
                case BodyIndex:
                    return new TextStructure(Value.TextBody, null, Scripter);
                case HtmlBodyIndex:
                    if (FullMessage == null)
                        FullMessage = Value.Folder.GetMessage(Value.UniqueId);
                    return new TextStructure(FullMessage.HtmlBody, null, Scripter);
                case PriorityIndex:
                    return new IntegerStructure(Value.Headers!=null?Value.Headers[HeaderId.Priority]:"0");
                case AttachmentsIndex:
                    return Value.MessageAttachments;
            }
            throw new ArgumentException($"Unknown index '{index}'", nameof(index));
        }

        public override void Set(Structure structure, string index = null)
        {
            if (structure?.Object == null)
            {
                throw new ArgumentNullException(nameof(structure));
            }

            switch (index.ToLower())
            {
                case SubjectIndex:
                    Value.Envelope.Subject = structure.ToString();
                    break;
                case DateIndex:
                    if (DateTimeOffset.TryParse(structure.ToString(), out DateTimeOffset dateTime))
                    {
                        Value.Envelope.Date = dateTime;
                    }
                    break;
                case ToIndex:
                    Value.Envelope.To.Clear();
                    Value.Envelope.To.Add(new MailboxAddress(structure.ToString()));
                    break;
                case FromIndex:
                    Value.Envelope.From.Clear();
                    Value.Envelope.From.Add(new MailboxAddress(structure.ToString()));
                    break;
                case BodyIndex:
                    Value.TextBody = structure.ToString();
                    break;
                case PriorityIndex:
                    Value.Headers[HeaderId.Priority] = structure.ToString();
                    break;
                default:
                    throw new ArgumentException($"Unknown index '{index}'", nameof(index));
            }
        }

        public override string ToString(string format = "")
        {
            return Value.NormalizedSubject;
        }

        protected override SimplifiedMessageSummary Parse(string value, string format = null)
        {
            throw new NotImplementedException();
        }
    }
}
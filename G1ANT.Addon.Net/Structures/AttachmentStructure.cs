using System;
using MailKit;
using MimeKit;
using System.IO;
using G1ANT.Language;
using G1ANT.Addon.Net.Models;
using G1ANT.Language.Services;

namespace G1ANT.Addon.Net
{
    [Structure(Name = "attachment", Priority = 999, Default = 0, AutoCreate = false, Tooltip = "The attachment structure stores current information about a mail attachment, which was downloaded with the `mail.` commands and stored in the `attachments` field of the mail structure")]
    public class AttachmentStructure : StructureTyped<IAttachmentModel>, IDisposable
    {
        private const string NameIndex = "name";
        private const string SizeIndex = "size";
        private const string TypeIndex = "type";
        private const string PathIndex = "path";
        private string pathToTempFile = "";

        public AttachmentStructure() : base(new MessageSummary(0)) { }

        public AttachmentStructure(IAttachmentModel value, string format = "", AbstractScripter scripter = null)
            : base(value, format)
        {
            Init();
        }

        public AttachmentStructure(object value, string format = "", AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        private void Init()
        {
            Indexes.Add(NameIndex);
            Indexes.Add(SizeIndex);
            Indexes.Add(TypeIndex);
            Indexes.Add(PathIndex);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                return this;

            switch (index.ToLower())
            {
                case NameIndex:
                    return new TextStructure(Value.Name, null, Scripter);
                case SizeIndex:
                    return new IntegerStructure(Value.Size, null, Scripter);
                case TypeIndex:
                    return new TextStructure(Value.Type, null, Scripter);
                case PathIndex:
                    return SaveAndGetAttachmentPath();
                default:
                    throw new ArgumentException($"Unknown index '{index}'", nameof(index));
            }
        }

        private PathStructure SaveAndGetAttachmentPath()
        {
            var tmpPath = Value.SaveAndGetPath();
            if (tmpPath != pathToTempFile)
            {
                DisposeAttachment(pathToTempFile);
                pathToTempFile = tmpPath;
            }
            return new PathStructure(pathToTempFile, null, Scripter);
        }

        public override string ToString(string format = "")
        {
            return Value.Name;
        }

        private void DisposeAttachment(string attachmentPath)
        {
            if (File.Exists(attachmentPath))
            {
                File.Delete(attachmentPath);
            }
        }

        public void Dispose()
        {
            DisposeAttachment(pathToTempFile);
        }
    }
}
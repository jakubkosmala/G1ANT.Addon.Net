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
        private const string AttachmentFilePrefix = "g1ant.attachment.";
        private const string AttachmentFilePostfix = ".temp";
        private string pathToTempFile = "";
        ITempFileService tempFileService = null;
        IGetMd5HashService getMd5HashService = null;

        public AttachmentStructure() : base(new MessageSummary(0)) { }

        public AttachmentStructure(IAttachmentModel value, string format = "", AbstractScripter scripter = null, ITempFileService iTempFileService = null, IGetMd5HashService iGetMd5HashService = null)
            : base(value, format)
        {
            Init(iTempFileService, iGetMd5HashService);
        }

        public AttachmentStructure(object value, string format = "", AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        private void Init(ITempFileService iTempFileService = null, IGetMd5HashService iGetMd5HashService = null)
        {
            Indexes.Add(NameIndex);
            Indexes.Add(SizeIndex);
            Indexes.Add(TypeIndex);
            Indexes.Add(PathIndex);
            tempFileService = iTempFileService ?? new TempFileService();
            getMd5HashService = iGetMd5HashService ?? new GetMd5HashService();
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new NotSupportedException("Index not found in AttachmentStructure");
            }

            switch (index.ToLower())
            {
                case NameIndex:
                    return new TextStructure(Value.Name, null, Scripter);
                case SizeIndex:
                    return new IntegerStructure(Value.Size, null, Scripter);
                case TypeIndex:
                    return new TextStructure(Value.Type, null, Scripter);
                case PathIndex:
                    pathToTempFile = SaveAndGetPath(Value.MimeEntity, Value.Name);
                    return new PathStructure(pathToTempFile, null, Scripter);
                default:
                    throw new ArgumentException($"Unknown index '{index}'", nameof(index));
            }

        }

        private string SaveAndGetPath(MimeEntity mimeEntity, string fileName)
        {
            var attachmentNameHash = GetAttachmentNameHash(fileName, mimeEntity.ContentId);
            var filePath = GetAttachmentTempFileNamePath($"{attachmentNameHash}.{fileName}");
            if (!File.Exists(filePath))
            {
                if (mimeEntity is MessagePart msgPart)
                {
                    using (var stream = File.Create(filePath))
                        msgPart.Message.WriteTo(stream);
                }
                else if (mimeEntity is MimePart mimePart)
                {
                    using (var stream = File.Create(filePath))
                        mimePart.Content.DecodeTo(stream);
                }
                else
                    return string.Empty;
            }
            return filePath;
        }

        public override string ToString(string format = "")
        {
            return Value.Name;
        }

        private string GetAttachmentTempFileNamePath(string attachmentNameHash)
        {
            return tempFileService.GetTempPath(AttachmentFilePrefix, attachmentNameHash, "");
        }

        private string GetAttachmentNameHash(string fileName, string contentId)
        {
            return getMd5HashService.GetMd5Hash($"{contentId}-{fileName}");
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
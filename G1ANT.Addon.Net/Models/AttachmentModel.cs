using G1ANT.Language.Services;
using MailKit;
using MimeKit;
using System;
using System.IO;

namespace G1ANT.Addon.Net.Models
{
    public class AttachmentModel : IAttachmentModel
    {
        public string Name { get; }
        public long Size { get; }
        public string Type { get; }

        private MimeEntity mimeEntity;
        private ITempFileService tempFileService = null;
        private IGetMd5HashService getMd5HashService = null;

        const string emptyAttachmentDefaultName = "unnamed-attachment";
        private const string AttachmentFilePrefix = "g1ant.attachment.";

        public AttachmentModel(MimeEntity mimeEntity, ITempFileService iTempFileService = null, IGetMd5HashService iGetMd5HashService = null)
        {
            tempFileService = iTempFileService ?? new TempFileService();
            getMd5HashService = iGetMd5HashService ?? new GetMd5HashService();
            this.mimeEntity = mimeEntity;
            Name = GetAttachmentName();
            Size = GetAttachmentSize();
            Type = GetAttachmentMediaTypeName();
        }

        public string SaveAndGetPath()
        {
            var attachmentNameHash = GetAttachmentNameHash(Name, mimeEntity.ContentId);
            var filePath = GetAttachmentTempFileNamePath($"{attachmentNameHash}.{Name}");
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
                    throw new InvalidDataException("Cannot retrieve attachment data");
            }
            return filePath;
        }

        private string GetAttachmentTempFileNamePath(string attachmentNameHash)
        {
            return tempFileService.GetTempPath(AttachmentFilePrefix, attachmentNameHash, "");
        }

        private string GetAttachmentNameHash(string fileName, string contentId)
        {
            if (string.IsNullOrEmpty(contentId))
                contentId = Guid.NewGuid().ToString();
            return getMd5HashService.GetMd5Hash($"{contentId}-{fileName}");
        }

        private string GetAttachmentName()
        {
            string fileName = "";
            if (!string.IsNullOrEmpty(mimeEntity.ContentDisposition?.FileName))
                fileName = mimeEntity.ContentDisposition.FileName;
            if (mimeEntity is MimePart part)
                fileName = part.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = emptyAttachmentDefaultName;
            }
            return fileName;
        }

        private long GetAttachmentSize()
        {
            if (!string.IsNullOrEmpty(mimeEntity.ContentDisposition?.FileName))
                return mimeEntity.ContentDisposition.Size.GetValueOrDefault(0);
            if (mimeEntity is MimePart part && part.Content != null && part.Content.Stream != null)
                return part.Content.Stream.Length;
            return 0;
        }

        private string GetAttachmentMediaTypeName()
        {
            return mimeEntity.ContentType.MediaType.ToString();
        }
    }
}

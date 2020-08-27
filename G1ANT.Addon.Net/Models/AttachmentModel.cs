using MailKit;
using MimeKit;

namespace G1ANT.Addon.Net.Models
{
    public class AttachmentModel : IAttachmentModel
    {
        public string Name { get; }
        public long Size { get; }
        public string Type { get; }
        public MimeEntity MimeEntity { get; }

        const string emptyAttachmentDefaultName = "unnamed-attachment";

        public AttachmentModel(MimeEntity mimeEntity)
        {
            Name = GetAttachmentName(mimeEntity);
            Size = GetAttachmentSize(mimeEntity);
            Type = GetAttachmentMediaTypeName(mimeEntity);
            MimeEntity = mimeEntity;
        }

        private string GetAttachmentName(MimeEntity mimeEntity)
        {
            var fileName = mimeEntity.ContentDisposition?.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = emptyAttachmentDefaultName;
            }
            return fileName;
        }

        private long GetAttachmentSize(MimeEntity mimeEntity)
        {
            var size = mimeEntity.ContentDisposition?.Size.GetValueOrDefault(0);
            return size ?? 0;
        }

        private string GetAttachmentMediaTypeName(MimeEntity mimeEntity)
        {
            return mimeEntity.ContentType.MediaType.ToString();
        }
    }
}

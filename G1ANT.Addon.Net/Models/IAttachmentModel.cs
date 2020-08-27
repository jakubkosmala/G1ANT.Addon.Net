using MimeKit;

namespace G1ANT.Addon.Net.Models
{

    public interface IAttachmentModel
    {
        string Name { get; }
        long Size { get; }
        string Type { get; }
        MimeEntity MimeEntity { get; }
    }
}

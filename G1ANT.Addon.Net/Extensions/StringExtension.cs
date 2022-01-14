using MailKit.Security;

namespace G1ANT.Addon.Net.Extensions
{
    public static class StringExtension
    {
        public static SecureSocketOptions ToSecureSocketOptions(this string value)
        {
            switch (value.ToLower())
            {
                case "ssl":
                    return SecureSocketOptions.SslOnConnect;
                case "tls":
                    return SecureSocketOptions.StartTls;
            }
            return SecureSocketOptions.Auto;
        }
    }
}

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util;
using MailKit.Net.Imap;
using MailKit.Security;
using System.Threading;

namespace G1ANT.Addon.Net.Models
{
    public class GMailOAuthModel : IAuthenticationModel
    {
        public string Username { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";

        public GMailOAuthModel()
        {
        }

        public void Authenticate(ImapClient client)
        {
            var secrets = new ClientSecrets
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret
            };

            var asyncRequest = GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets, 
                new[] { "https://mail.google.com/" }, 
                Username,
                CancellationToken.None);
            var googleCredentials = asyncRequest.GetAwaiter().GetResult();

            if (googleCredentials.Token.IsExpired(SystemClock.Default))
            {
                googleCredentials.RefreshTokenAsync(CancellationToken.None).GetAwaiter().GetResult();
            }

            var oauth2 = new SaslMechanismOAuth2(googleCredentials.UserId, googleCredentials.Token.AccessToken);
            client.Authenticate(oauth2);
        }
    }
}

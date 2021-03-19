using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.Identity.Client;
using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace G1ANT.Addon.Net.Models
{
    public class OfficeOAuthModel : IAuthenticationModel
    {
        public string Username { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string TenantId { get; set; } = "";
        public string Password { get; set; } = "";

        public OfficeOAuthModel()
        {
        }

        private IPublicClientApplication BuildPublicClientApplication()
        {
            var options = new PublicClientApplicationOptions
            {
                ClientId = ClientId,
                TenantId = TenantId,
                RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
            };

            return PublicClientApplicationBuilder.CreateWithApplicationOptions(options).Build();
        }

        private string[] GetScopes()
        {
            var scopes = new string[] {
                "https://outlook.office.com/IMAP.AccessAsUser.All", // Only needed for IMAP
                //"https://outlook.office.com/POP.AccessAsUser.All",  // Only needed for POP
                //"https://outlook.office.com/SMTP.Send", // Only needed for SMTP
            };

            return scopes;
        }

        private async Task<string> GetOAuthTokenWithPassword()
        {
            SecureString securePassword = new SecureString();
            Array.ForEach(Password.ToCharArray(), securePassword.AppendChar);
            var app = BuildPublicClientApplication();
            var authenticationResult = await app.AcquireTokenByUsernamePassword(GetScopes(), Username, securePassword).ExecuteAsync(CancellationToken.None);
            return authenticationResult.AccessToken;
        }

        private async Task<string> GetOAuthToken()
        {
            var app = BuildPublicClientApplication();
            var authResult = await app.AcquireTokenInteractive(GetScopes()).WithLoginHint(Username).ExecuteAsync();
            return authResult.AccessToken;
        }

        private string GetToken()
        {
            Task<string> task;
            if (string.IsNullOrEmpty(Password))
                task = Task.Run(async () => await GetOAuthToken());
            else
                task = Task.Run(async () => await GetOAuthTokenWithPassword());
            return task.Result;
        }

        public void Authenticate(ImapClient client)
        {
            string token = GetToken();
            client.Authenticate(new SaslMechanismOAuth2(Username, token));
        }
    }
}

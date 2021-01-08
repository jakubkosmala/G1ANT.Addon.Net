using MailKit;
using MailKit.Net.Imap;
using System;
using System.Net;
using Microsoft.Identity.Client;
using System.Threading;
using MailKit.Security;
using System.Security;
using G1ANT.Addon.Net.Models;

namespace G1ANT.Addon.Net
{
    public sealed class ImapManager
    {
        private IAuthenticationModel authenticator;
        private ImapClient client;
        private Uri uri;

        public static ImapManager Instance { get; } = new ImapManager();

        private void ConnectClient(ImapClient client)
        {
            client.Connect(uri);
            authenticator?.Authenticate(client);
            client.Inbox.Open(FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        private string GetOAuthToken(string appClientId, string username, string password)
        {
            SecureString securePassword = new SecureString();

            Array.ForEach(password.ToCharArray(), securePassword.AppendChar);
            var scopes = new[] { "https://outlook.office365.com/IMAP.AccessAsUser.All" };
            var app = PublicClientApplicationBuilder.Create(appClientId).WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs).Build();
            var asyncRequest = app.AcquireTokenByUsernamePassword(scopes, username, securePassword).ExecuteAsync(CancellationToken.None);
            var authenticationResult = asyncRequest.GetAwaiter().GetResult();

            return authenticationResult.AccessToken;
        }

        private void Authenticate(ImapClient client)
        {
            string UserName = "";
            string ClientId = "";
            string Password = "";

            // Get an OAuth token:
            var token = GetOAuthToken(ClientId, UserName, Password);
            client.Authenticate(new SaslMechanismOAuth2(UserName, token));
        }

        public void DisconnectClient()
        {
            client.Disconnect(true);
        }

        public ImapClient CreateImapClient(IAuthenticationModel authenticator, Uri uri, int timeout)
        {
            var client = new ImapClient { Timeout = timeout };
            this.authenticator = authenticator;
            this.client = client;
            this.uri = uri;
            ConnectClient(this.client);
            return this.client;
        }

        public ImapClient GetClient()
        {
            return client;
        }

        public void Reconnect()
        {
            if (!client.IsConnected)
                ConnectClient(client);
        }
    }
}

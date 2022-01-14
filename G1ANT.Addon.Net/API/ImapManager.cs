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

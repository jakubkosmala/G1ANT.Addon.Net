using MailKit;
using MailKit.Net.Imap;
using System;
using System.Net;

namespace G1ANT.Addon.Net
{
    public sealed class ImapManager
    {
        private static ImapManager instance = null;
        private static readonly object padLock = new object();
        private NetworkCredential credentials;
        private ImapClient client;
        private Uri uri;

        public static ImapManager Instance
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new ImapManager();
                    }
                    return instance;
                }
            }
        }

        private void ConnectClient(ImapClient client)
        {
            client.Connect(uri);
            client.Authenticate(credentials);
            client.Inbox.Open(FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        public void DisconnectClient()
        {
            client.Disconnect(true);
        }

        public ImapClient CreateImapClient(NetworkCredential credentials, Uri uri, int timeout)
        {
            var client = new ImapClient { Timeout = timeout };
            this.credentials = credentials;
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
            ConnectClient(client);
        }
    }
}

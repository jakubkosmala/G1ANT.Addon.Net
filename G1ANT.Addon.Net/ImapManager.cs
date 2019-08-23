using MailKit;
using MailKit.Net.Imap;
using System;
using System.Net;

namespace G1ANT.Addon.Net
{
    public sealed class ImapManager
    {
        private static ImapManager m_oInstance = null;
        private static readonly object m_oPadLock = new object();
        private static NetworkCredential _credentials;
        private static ImapClient _client;
        private static Uri _uri;

        public static ImapManager Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new ImapManager();
                    }
                    return m_oInstance;
                }
            }
        }

        private void ConnectClient(ImapClient client)
        {
            client.Connect(_uri);
            client.Authenticate(_credentials);
            client.Inbox.Open(FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        public void DisconnectClient()
        {
            _client.Disconnect(true);
        }

        public ImapClient CreateImapClient(NetworkCredential credentials, Uri uri, int timeout)
        {
            var client = new ImapClient { Timeout = timeout };
            _credentials = credentials;
            _client = client;
            _uri = uri;
            ConnectClient(_client);
            return _client;
        }

        public ImapClient GetClient()
        {
            return _client;
        }

        public void Reconnect()
        {
            ConnectClient(_client);
        }
    }
}

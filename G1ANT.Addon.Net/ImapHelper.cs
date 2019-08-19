using MailKit;
using MailKit.Net.Imap;
using System;
using System.Net;

namespace G1ANT.Addon.Net
{
    public static class ImapHelper
    {
        private static NetworkCredential _credentials;
        private static ImapClient _client;
        private static Uri _uri;

        private static void ConnectClient(ImapClient client)
        {
            client.Connect(_uri);
            client.Authenticate(_credentials);
            client.Inbox.Open(FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        public static void DisconnectClient()
        {
            _client.Disconnect(true);
        }

        public static ImapClient CreateImapClient(NetworkCredential credentials, Uri uri, int timeout)
        {
            var client = new ImapClient {Timeout = timeout};
            _credentials = credentials;
            _client = client;
            _uri = uri;
            ConnectClient(_client);
            return _client;
        }

        public static ImapClient GetClient()
        {
            return _client;
        }

        public static void Reconnect()
        {
            ConnectClient(_client);
        }
    }
}

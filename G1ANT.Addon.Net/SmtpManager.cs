using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Net
{
    public sealed class SmtpManager
    {
        private NetworkCredential credentials;
        private SmtpClient client;
        private string host;
        private int port;
        private SecureSocketOptions socketOptions = SecureSocketOptions.Auto;

        public static SmtpManager Instance { get; } = new SmtpManager();

        private void ConnectClient(SmtpClient client)
        {
            client.Connect(host, port, socketOptions);
            client.Authenticate(credentials);
        }

        public void DisconnectClient()
        {
            client.Disconnect(true);
        }

        public SmtpClient CreateImapClient(NetworkCredential credentials, string host, int port, SecureSocketOptions options, int timeout)
        {
            var client = new SmtpClient { Timeout = timeout };
            this.credentials = credentials;
            this.client = client;
            this.host = host;
            this.port = port;
            this.socketOptions = options;
            ConnectClient(this.client);
            return this.client;
        }

        public SmtpClient GetClient()
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

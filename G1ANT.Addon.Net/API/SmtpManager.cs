using G1ANT.Addon.Net.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using System;
using System.Net;

namespace G1ANT.Addon.Net
{
    public sealed class SmtpManager
    {
        private IAuthenticationModel authenticator;
        private SmtpClient client;
        private string host;
        private int port;
        private SecureSocketOptions socketOptions = SecureSocketOptions.Auto;

        public static SmtpManager Instance { get; } = new SmtpManager();

        private void ConnectClient(SmtpClient client)
        {
            client.Connect(host, port, socketOptions);
            authenticator?.Authenticate(client);
        }

        public void DisconnectClient()
        {
            client.Disconnect(true);
        }

        public SmtpClient CreateSmtpClient(IAuthenticationModel authenticator, string host, int port, SecureSocketOptions options, int timeout)
        {
            var client = new SmtpClient { Timeout = timeout };
            this.authenticator = authenticator;
            this.client = client;
            this.host = host;
            this.port = port;
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

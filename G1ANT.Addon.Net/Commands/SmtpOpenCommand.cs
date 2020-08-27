/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using MailKit.Security;
using System.Net;

namespace G1ANT.Addon.Net.Commands
{
    [Command(Name = "smtp.open", Tooltip = "This command opens SMTP connection with the server")]
    public class SmtpOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "SMTP server address")]
            public TextStructure Host { get; set; }

            [Argument(Tooltip = "SMTP server port number")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Tooltip = "Socket options, can be SSL, TLS or empty for none of them.")]
            public TextStructure Options { get; set; } = new TextStructure();

            [Argument(Required = true, Tooltip = "User login")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "User password")]
            public TextStructure Password { get; set; }

            [Argument(Tooltip = "If set to `true`, the command will ignore any security certificate errors")]
            public BooleanStructure IgnoreCertificateErrors { get; set; } = new BooleanStructure(false);
        }

        public SmtpOpenCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            if (arguments.IgnoreCertificateErrors.Value)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            SecureSocketOptions optons = SecureSocketOptions.Auto;
            switch (arguments.Options.Value.ToLower())
            {
                case "ssl":
                    optons = SecureSocketOptions.SslOnConnect;
                    break;
                case "tls":
                    optons = SecureSocketOptions.StartTls;
                    break;
            }

            SmtpManager.Instance.CreateImapClient(credentials, arguments.Host.Value, arguments.Port.Value, optons, timeout);
        }
    }
}

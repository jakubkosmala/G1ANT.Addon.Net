/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using G1ANT.Language;
using System.Net;

namespace G1ANT.Addon.Net
{
    [Command(Name = "imap.open", Tooltip = "This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time span, with the option to consider only unread messages and/or mark all of the checked ones as read")]
    public class ImapOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "IMAP server address")]
            public TextStructure Host { get; set; }

            [Argument(Tooltip = "IMAP server port number")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Tooltip = "IMAP server port number")]
            public BooleanStructure UseSsl { get; set; } = new BooleanStructure(true);

            [Argument(Tooltip = "User login")]
            public TextStructure Login { get; set; }

            [Argument(Tooltip = "User password")]
            public TextStructure Password { get; set; }

            [Argument(Tooltip = "If set to `true`, the command will ignore any security certificate errors")]
            public BooleanStructure IgnoreCertificateErrors { get; set; } = new BooleanStructure(false);
        }

        public ImapOpenCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            if (arguments.IgnoreCertificateErrors.Value)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            NetworkCredential credentials = null;
            if (!string.IsNullOrEmpty(arguments.Login?.Value) && !string.IsNullOrEmpty(arguments.Password?.Value))
                credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder(arguments.UseSsl.Value ? "imaps" : "imap", arguments.Host.Value, arguments.Port.Value).Uri;
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;

            ImapManager.Instance.CreateImapClient(credentials, uri, timeout);
        }
    }
}

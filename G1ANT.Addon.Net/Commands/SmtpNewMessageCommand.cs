/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Addon.Net.Extensions;
using G1ANT.Language;
using MimeKit;
using System;

namespace G1ANT.Addon.Net.Commands
{
    [Command(Name = "smtp.newmessage", Tooltip = "This command creates new empty message which can be modified and send using smtp.send command")]
    public class SmtpNewMessageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Sender's email address")]
            public TextStructure From { get; set; }

            [Argument(Tooltip = "Recipient's email address as text or list of addressess")]
            public Structure To { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Carbon copy address as text or list of addressess")]
            public Structure Cc { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Blind carbon copy address as text or list of addressess")]
            public Structure Bcc { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Message subject")]
            public TextStructure Subject { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Message body, i.e. the main content of an email")]
            public TextStructure TextBody { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Message html body, i.e. the main content of an email")]
            public TextStructure HtmlBody { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "List of full paths to all files to be attached")]
            public ListStructure Attachments { get; set; }

            [Argument(Required = false, Tooltip = "Name of a variable where the new message will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public SmtpNewMessageCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            var message = new SimplifiedMessageSummary();
            if (string.IsNullOrWhiteSpace(arguments.From?.Value))
                throw new ArgumentException("From argument cannot be empty");
            message.Sender = new MailboxAddress(arguments.From.Value);
            message.From.Add(new MailboxAddress(arguments.From.Value));
            message.To.SetMailboxesFromStructure(arguments.To, "To");
            message.Cc.SetMailboxesFromStructure(arguments.Cc, "Cc");
            message.Bcc.SetMailboxesFromStructure(arguments.Bcc, "Bcc");
            message.Subject = arguments.Subject.Value;
            message.TextBody = arguments.TextBody.Value;
            message.HtmlBody = arguments.HtmlBody.Value;
            message.Attachments = arguments.Attachments?.Value;

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new MailStructure(message));
        }
    }
}

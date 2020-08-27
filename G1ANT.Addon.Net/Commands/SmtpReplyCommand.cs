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
using MimeKit;
using System;

namespace G1ANT.Addon.Net.Commands
{
    [Command(Name = "smtp.reply", Tooltip = "This command creates new reply message based on received message which can be modified and send using smtp.send command")]
    public class SmtpReplyCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Sender's email address")]
            public TextStructure From { get; set; }

            [Argument(Required = true, Tooltip = "Message to reply to")]
            public MailStructure Mail { get; set; }

            [Argument(Required = false, Tooltip = "Reply to all recipients or only to the sender")]
            public BooleanStructure ReplyToAll { get; set; } = new BooleanStructure(false);

            [Argument(Required = false, Tooltip = "Prefix added to the subject of reply message")]
            public TextStructure SubjectPrefix { get; set; } = new TextStructure("Re: ");

            [Argument(Required = false, Tooltip = "Name of a variable where the reply message will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public SmtpReplyCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments.From?.Value))
                throw new ArgumentException("From argument cannot be empty");
            var reply = arguments.Mail.CreateReply(arguments.ReplyToAll.Value, arguments.SubjectPrefix.Value);
            reply.Value.Sender = new MailboxAddress(arguments.From.Value);
            reply.Value.From.Add(new MailboxAddress(arguments.From.Value));
            Scripter.Variables.SetVariableValue(arguments.Result.Value, reply);
        }
    }
}

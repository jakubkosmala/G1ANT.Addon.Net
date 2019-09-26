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
using System.Collections.Generic;
using System.Linq;
using G1ANT.Language;
using G1ANT.Language.Models;
using MailKit;
using MailKit.Search;

namespace G1ANT.Addon.Net
{
    [Command(Name = "imap.getmails", Tooltip = "This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time span, with the option to consider only unread messages and/or mark all of the checked ones as read")]
    public class ImapGetMailsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Folder to fetch emails from")]
            public TextStructure Folder { get; set; } = new TextStructure("INBOX");

            [Argument(Tooltip = "How many emails to download")]
            public IntegerStructure Count { get; set; } = new IntegerStructure(50);

            [Argument(Required = false, Tooltip = "Start downloading emails newer than a specified one")]
            public SimplifiedMessageSummary FromEmail { get; set; }

            [Argument(Required = false, Tooltip = "Start date for messages to be checked")]
            public DateStructure SinceDate { get; set; }

            [Argument(Tooltip = "Ending date for messages to be checked")]
            public DateStructure ToDate { get; set; } = new DateStructure(DateTime.Now);

            [Argument(Required = false, Tooltip = "If set to `true`, only unread messages will be checked")]
            public BooleanStructure OnlyUnreadMessages { get; set; } = new BooleanStructure(false);

            [Argument(Required = false, Tooltip = "Mark processed messages as read")]
            public BooleanStructure MarkAsRead { get; set; } = new BooleanStructure(true);

            [Argument(Required = false, Tooltip = "Name of a list variable where the returned mail variables will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public ImapGetMailsCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            var markAllMessagesAsRead = arguments.MarkAsRead.Value;

            var client = ImapManager.Instance.GetClient();

            if (client.IsConnected && client.IsAuthenticated)
            {
                var folder = client.GetFolder(arguments.Folder.Value);
                folder.Open(FolderAccess.ReadWrite);
                var messages = ReceiveMesssages(folder, arguments);
                SendMessageListToScripter(folder, arguments, messages);

                if (markAllMessagesAsRead)
                {
                    MarkMessagesAsRead(folder, messages);
                }
            }
            else
            {
                throw new Exception("Could not connect or authenticate on the server");
            }
        }

        private void SendMessageListToScripter(IMailFolder folder, Arguments arguments, List<IMessageSummary> messages)
        {
            var messageList = CreateMessageStructuresFromMessages(folder, messages);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, messageList);
        }

        private ListStructure CreateMessageStructuresFromMessages(IMailFolder folder, List<IMessageSummary> messages)
        {
            var messageList = new ListStructure();
            foreach (var message in messages)
            {
                var attachments = CreateAttachmentStructuresFromAttachments(message, folder, message.Attachments);
                var messageWithFolder = new SimplifiedMessageSummary(message as MessageSummary, folder, attachments);
                var structure = new MailStructure(messageWithFolder, null, null);
                messageList.AddItem(structure);
            }
            return messageList;
        }

        private ListStructure CreateAttachmentStructuresFromAttachments(IMessageSummary message, IMailFolder folder,
            IEnumerable<BodyPartBasic> attachments)
        {
            ListStructure attachmentsList = new ListStructure();

            foreach (var attachment in attachments)
            {
                AttachmentModel attachmentModel = new AttachmentModel(attachment, folder, message);
                AttachmentStructure temp = new AttachmentStructure(attachmentModel);
                attachmentsList.AddItem(temp);
            }
            return attachmentsList;
        }

        private List<IMessageSummary> ReceiveMesssages(IMailFolder folder, Arguments arguments)
        {
            var options = MessageSummaryItems.All
                | MessageSummaryItems.Body
                | MessageSummaryItems.BodyStructure
                | MessageSummaryItems.UniqueId;

            var query = CreateSearchQuery(arguments);

            var uids = folder.Search(query).Take(arguments.Count.Value).ToList();

            if (arguments.FromEmail != null)
            {
                var fromMailIndex = uids.FindIndex(u => u == arguments.FromEmail.UniqueId);

                uids.RemoveRange(0, fromMailIndex);
            }

            if (uids.Count > arguments.Count.Value)
            {
                uids.RemoveRange(arguments.Count.Value, uids.Count - arguments.Count.Value);
            }

            return folder.Fetch(uids, options).ToList();
        }

        private static void MarkMessagesAsRead(IMailFolder folder, List<IMessageSummary> messages)
        {
            foreach (var message in messages)
            {
                folder.SetFlags(message.UniqueId, MessageFlags.Seen, true);
            }
        }

        private static SearchQuery CreateSearchQuery(Arguments arguments)
        {
            return SearchQuery
                .DeliveredAfter(arguments.SinceDate?.Value ?? DateTime.MinValue)
                .And(arguments.OnlyUnreadMessages.Value ? SearchQuery.NotSeen : SearchQuery.All)
                .And(SearchQuery.DeliveredBefore(arguments.ToDate.Value));
        }
    }
}

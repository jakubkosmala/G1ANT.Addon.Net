

using G1ANT.Language;
/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
namespace G1ANT.Addon.Net
{
    [Command(Name = "imap.reconnect", Tooltip = "This command restores imap connection to mail server")]
    public class ImapReconnectCommand : Command
    {
        public class Arguments : CommandArguments
        { }

        public ImapReconnectCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            ImapHelper.Reconnect();
        }
    }
}



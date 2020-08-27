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

namespace G1ANT.Addon.Net.Commands
{
    [Command(Name = "smtp.close", Tooltip = "This command closes SMTP connection to mail server")]
    public class SmtpCloseCommand : Command
    {
        public class Arguments : CommandArguments
        { }

        public SmtpCloseCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            SmtpManager.Instance.DisconnectClient();
        }
    }
}

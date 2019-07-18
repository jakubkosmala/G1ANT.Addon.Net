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
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace G1ANT.Addon.Net
{
    [Command(Name = "as400.open", Tooltip = "This command opens a terminal connection to work with the IBM AS/400 server", NeedsDelay = true)]
    public class AS400openCommand : Command
    {
        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "IP or Hostname required to establish a connection")]
            public TextStructure Host { get; set; }

        }

        public AS400openCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var pathToTelnet = Path.Combine(AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName, @"telnet.exe");
            var pathToPutty = Path.Combine(AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName, @"putty.exe");
            var errorJump = arguments.ErrorJump != null && arguments.ErrorJump != null ? arguments.ErrorJump.ToString() : string.Empty;
            var host = arguments.Host.Value;
            //telnet = System.Diagnostics.Process.Start(pathToTelnet, host);
            var putty = Process.Start(pathToPutty, "-load AS400");
            Thread.Sleep(2000);
            //RobotWin32.ShowWindow(telnet.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);          
            //IntPtr iHandle = RobotWin32.FindWindow(null, telnet.MainWindowTitle);
            RobotWin32.ShowWindow(putty.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);
            var iHandle = RobotWin32.FindWindow(null, putty.MainWindowTitle);
            RobotWin32.SetForegroundWindow(iHandle);
        }
    }
}


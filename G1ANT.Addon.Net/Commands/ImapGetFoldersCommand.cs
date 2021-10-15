using G1ANT.Language;
using System.Linq;

namespace G1ANT.Addon.Net.Commands
{
    [Command(Name = "imap.getfolders", Tooltip = "This command returns all account personal folders.")]
    public class ImapGetFoldersCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Name of a list variable where the returned folders will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public ImapGetFoldersCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            var folders = ImapManager.Instance.GetPersonalFolders();
            var result = new ListStructure(folders.ToList<object>(), "", Scripter);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
        }
    }
}

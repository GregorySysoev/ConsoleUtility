using ConsoleUtility;

namespace ConsoleUtility
{
    [CommandPrefix(new string[] { "-d", "--depth" })]
    public class CommandSetDepth : ICommand, ICommandFindInTreeType
    {
        public int Value { get; set; }

        public CommandSetDepth()
        {
            Value = -1;
        }
    }
}
using ConsoleUtility;
namespace ConsoleUtility
{
    [CommandPrefix(new string[] { "-?", "--help" })]
    // [CommandPrefix("-?")]
    public class HelpCommand : ICommand
    {

    }
}
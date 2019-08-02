using ConsoleUtility;
namespace ConsoleUtility
{
    public class HelpCommand : ICommand
    {
        public void Execute(IWriter writer)
        {
            writer.Write("List of available commands:");
            writer.Write("");
            writer.Write("Find [args] - find string inf file");
            writer.Write("");
            writer.Write("List of available args:");
            writer.Write("-t [num] set count of threads");
            writer.Write("-s [string] set search string");
        }
    }
}
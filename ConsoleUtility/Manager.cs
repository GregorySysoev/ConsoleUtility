using ConsoleUtility;
namespace ConsoleUtility
{
    public class Manager
    {
        public ICommand command;
        public void RunCommand()
        {
            command.Execute();
        }
        public void IdentifyCommand(string[] argumentsOfCommandLine)
        {
            var parser = new Parser();
            command = parser.Parse(argumentsOfCommandLine);
        }
        public Manager()
        {
        }
    }
}
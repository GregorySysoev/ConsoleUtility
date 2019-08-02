using ConsoleUtility;
namespace ConsoleUtility
{
    public class Manager
    {
        private ICommand _command;
        private IWriter _writer;
        public void RunCommand()
        {
            _command.Execute(_writer);
        }
        public void IdentifyCommand(string[] argumentsOfCommandLine)
        {
            var parser = new Parser();
            _command = parser.Parse(argumentsOfCommandLine);
        }
        public Manager(IWriter writer)
        {
            _writer = writer;
        }
    }
}
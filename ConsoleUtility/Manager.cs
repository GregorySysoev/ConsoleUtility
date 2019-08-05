using ConsoleUtility;
using System.Collections.Generic;
namespace ConsoleUtility
{
    public class Manager
    {
        public List<ICommand> _command;
        private IWriter _writer;
        public void RunCommand()
        {
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
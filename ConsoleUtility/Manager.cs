using ConsoleUtility;
using System.Collections.Generic;
namespace ConsoleUtility
{
    public class Manager
    {
        public List<ICommand> _command;
        private IPrinter _printer;
        public void RunCommands()
        {
            Executor executor = new Executor(_command, _printer);
            executor.Execute();
        }
        public void IdentifyCommand(string[] argumentsOfCommandLine)
        {
            var parser = new Parser();
            _command = parser.Parse(argumentsOfCommandLine);
        }
        public Manager(IPrinter printer)
        {
            _printer = printer;
        }
    }
}
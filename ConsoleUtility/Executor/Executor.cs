using ConsoleUtility;
using System.Collections.Generic;
namespace ConsoleUtility
{
    public class Executor
    {
        private List<ICommand> _commands;
        private IWriter _writer;
        public Executor(List<ICommand> commands, IWriter writer)
        {
            _commands = commands;
            _writer = writer;
        }
        public void Execute()
        {
        }
    }
}
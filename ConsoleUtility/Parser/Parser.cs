using System;

using ConsoleUtility;

namespace ConsoleUtility
{
    public class Parser
    {
        private ICommand _command;
        public ICommand Parse(string[] args)
        {
            if (args.Length == 0)
            {
                return new ErrorCommand();
            }

            _command = IdentifyCommand(args[0]);

            for (int i = 1; i < args.Length; i++)
            {
                ParseArgumentOfCommand(args[i]);
            }
            return _command;
        }
        public Parser()
        {

        }

        public ICommand IdentifyCommand(string possibleCommandName)
        {
            var commandName = possibleCommandName + "Command";
            var type = Type.GetType(commandName);

            var result = (ICommand)Activator.CreateInstance(type);
            return result;
        }

        public void ParseArgumentOfCommand(string arg)
        {
        }
    }
}
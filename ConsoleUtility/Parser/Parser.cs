using System;
using System.Collections;
using ConsoleUtility;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUtility
{
    public class Parser
    {
        public List<ICommand> Parse(string[] args)
        {
            List<ICommand> commands = new List<ICommand>();
            if (args == null)
            {
                commands.Add(new ErrorCommand());
                return commands;
            }

            var commandAvailableList = GetCommandsAvailableList();
            foreach (var arg in args)
            {
                foreach (var commandAvailable in commandAvailableList)
                {
                    var attribute = (CommandPrefixAttribute)typeof(ICommand)
                    .GetCustomAttributes(typeof(CommandPrefixAttribute), false)?.First();
                    commands.Add(commandAvailable);
                }
            }

            return commands;
        }
        public Parser()
        {

        }

        public List<ICommand> GetCommandsAvailableList()
        {
            var assembly = typeof(ICommand).Assembly;
            var types = assembly.GetTypes()
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface)
                .Select(c => (ICommand)Activator.CreateInstance(c))
                .ToList();

            return types;
        }
    }
}
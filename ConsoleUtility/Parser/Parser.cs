using System;
using System.Collections;
using ConsoleUtility;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                    if (commandAvailable.GetType().GetCustomAttribute<CommandPrefixAttribute>() == null)
                    {
                        continue;
                    }

                    bool command = commandAvailable.GetType().GetCustomAttribute<CommandPrefixAttribute>().prefix.Contains(arg);
                    if (command && !commands.Contains(commandAvailable))
                    {
                        commands.Add(commandAvailable);
                    }
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
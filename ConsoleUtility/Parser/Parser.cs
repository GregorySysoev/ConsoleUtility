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
        private bool _wasError = false;
        public List<ICommand> resultCommands = new List<ICommand>();

        public List<ICommand> GetCommandsAvailableList()
        {
            var assembly = typeof(ICommand).Assembly;
            var types = assembly.GetTypes()
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface)
                .Select(c => (ICommand)Activator.CreateInstance(c))
                .ToList();

            return types;
        }

        public Parser()
        {

        }
        public void setValueToCommand(string arg)
        {
            if (resultCommands.Last().GetType().GetProperty("Value") != null)
            {
                Type commandWithValue = resultCommands.Last().GetType();
                PropertyInfo property = commandWithValue.GetProperty("Value");
                Type propertyType = property.PropertyType;

                try
                {
                    Convert.ChangeType(arg, propertyType);
                }
                catch (Exception)
                {
                    _wasError = true;
                }
                property.SetValue(resultCommands.Last(), Convert.ChangeType(arg, propertyType));
            }
            else
            {
                _wasError = true;
            }
        }
        public bool compareCommandsInListAndArgs(ICommand command, string arg)
        {
            bool commandDecided = command
                       .GetType()
                       .GetCustomAttribute<CommandPrefixAttribute>()
                       .prefix
                       .Contains(arg);

            return commandDecided;
        }
        public List<ICommand> Parse(string[] args)
        {
            if (args == null)
            {
                resultCommands.Add(new ErrorCommand());
                return resultCommands;
            }

            var commandAvailableList = GetCommandsAvailableList();
            int i = 0, j = 0, nextArgIndex = 0;

            for (j = 0; (j < commandAvailableList.Count) && (resultCommands.Count < args.Length) && (!_wasError); j++)
            {
                if (commandAvailableList[j].GetType().GetCustomAttribute<CommandPrefixAttribute>() == null)
                {
                    continue;
                }

                for (i = 0; i < args.Length; i++)
                {
                    if (compareCommandsInListAndArgs(commandAvailableList[j], args[i]))
                    {
                        if (!resultCommands.Contains(commandAvailableList[j]))
                        {
                            resultCommands.Add(commandAvailableList[j]);
                        }
                        break;
                    }

                    if (resultCommands.Count != 0)
                    {
                        setValueToCommand(args[i]);
                        break;
                    }
                }
            }

            if ((i < args.Length - 1))
            {
                if (resultCommands.Count != 0)
                {
                    setValueToCommand(args[++i]);
                }
            }

            if ((_wasError == true) | (resultCommands.Count == 0))
            {
                return new List<ICommand>()
                    {
                        new ErrorCommand()
                    };
            }
            return resultCommands;
        }
    }
}
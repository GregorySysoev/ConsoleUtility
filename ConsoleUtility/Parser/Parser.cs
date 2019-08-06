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
        private List<string> _attributesOfCommandsList = new List<String>();
        private List<ICommand> _commandAvailableList = new List<ICommand>();


        public List<ICommand> resultCommands = new List<ICommand>();




        public void GetAttributesOfCommandsList()
        {
            foreach (var command in _commandAvailableList)
            {
                var a = command.GetType().GetCustomAttribute<CommandPrefixAttribute>();
                if (a != null)
                {
                    _attributesOfCommandsList.AddRange(a.prefix.ToList());
                }
            }
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
        public void SetValueToCommand(string arg)
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
        public bool CompareCommandsInListAndArgs(ICommand command, string arg)
        {
            bool commandDecided = command
                       .GetType()
                       .GetCustomAttribute<CommandPrefixAttribute>()
                       .prefix
                       .Contains(arg);

            return commandDecided;
        }

        public ICommand SelectCommandByArg(string arg)
        {
            foreach (var command in _commandAvailableList)
            {
                if (command.GetType().GetCustomAttribute<CommandPrefixAttribute>() == null)
                {
                    continue;
                }

                if (command.GetType().GetCustomAttribute<CommandPrefixAttribute>().prefix.Contains(arg))
                {
                    return command;
                }
            }
            return null;
        }
        public Parser()
        {
            _commandAvailableList = GetCommandsAvailableList();
            GetAttributesOfCommandsList();
        }
        public List<ICommand> Parse(string[] args)
        {
            if (args == null || _commandAvailableList.Count == 0)
            {
                resultCommands.Add(new ErrorCommand());
                return resultCommands;
            }

            for (int i = 0; i < args.Length && !_wasError; i++)
            {
                if (_attributesOfCommandsList.Contains(args[i]))
                {
                    var com = SelectCommandByArg(args[i]);
                    if (!resultCommands.Contains(com))
                    {
                        resultCommands.Add(com);
                    }
                    continue;
                }

                if (resultCommands.Count != 0)
                {
                    SetValueToCommand(args[i]);
                }
                else
                {
                    _wasError = true;
                }
            }
            if (_wasError)
            {
                resultCommands = new List<ICommand>() { new ErrorCommand() };
            }
            return resultCommands;
        }
    }
}
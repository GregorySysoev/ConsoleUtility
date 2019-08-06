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
        private Dictionary<string[], Type> commandsWithAttributes = new Dictionary<string[], Type>();
        public List<ICommand> resultCommands = new List<ICommand>();

        public void GetCommandsWithAttributes()
        {
            var assembly = typeof(ICommand).Assembly;
            var types = assembly.GetTypes()
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface);

            foreach (var type in types)
            {
                if (type.GetCustomAttribute<CommandPrefixAttribute>() != null)
                {
                    commandsWithAttributes.Add(type.GetCustomAttribute<CommandPrefixAttribute>().prefix, type);
                }
            }
        }
        public void SetValueToCommand(string arg, ICommand command)
        {
            Type commandWithValue = command.GetType();
            PropertyInfo property = commandWithValue.GetProperty("Value");
            if (property != null)
            {
                Type propertyType = property.PropertyType;
                try
                {
                    var a = Convert.ChangeType(arg, propertyType);
                    property.SetValue(resultCommands.Last(), a);
                }
                catch (Exception)
                {
                    _wasError = true;
                }
            }
            else
            {
                _wasError = true;
            }
        }
        public ICommand FindCommandUsingAttribute(string attr)
        {
            foreach (var element in commandsWithAttributes)
            {
                if (element.Key.Contains(attr))
                {
                    return (ICommand)Activator.CreateInstance(element.Value);
                }
            }
            return null;
        }
        public Parser()
        {
            GetCommandsWithAttributes();
        }
        public List<ICommand> Parse(string[] args)
        {
            if (args == null || commandsWithAttributes.Count == 0)
            {
                resultCommands.Add(new ErrorCommand());
                return resultCommands;
            }

            for (int i = 0; i < args.Length && !_wasError; i++)
            {
                if (FindCommandUsingAttribute(args[i]) is ICommand command)
                {
                    resultCommands.Add(command);
                    continue;
                }

                if (resultCommands.Count != 0 && i != args.Length)
                {
                    SetValueToCommand(args[i], resultCommands.Last());
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
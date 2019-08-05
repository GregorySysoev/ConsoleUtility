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
            List<ICommand> resultCommands = new List<ICommand>();

            if (args == null)
            {
                resultCommands.Add(new ErrorCommand());
                return resultCommands;
            }

            var commandAvailableList = GetCommandsAvailableList();
            bool wasError = false;
            int i = 0, j = 0;
            for (j = 0; j < commandAvailableList.Count & !wasError; j++)
            {
                if (commandAvailableList[j].GetType().GetCustomAttribute<CommandPrefixAttribute>() == null)
                {
                    continue;
                }
                for (i = 0; i < args.Length; i++)
                {
                    bool commandDecided = commandAvailableList[j]
                        .GetType()
                        .GetCustomAttribute<CommandPrefixAttribute>()
                        .prefix
                        .Contains(args[i]);

                    if (commandDecided)
                    {
                        if (!resultCommands.Contains(commandAvailableList[j]))
                        {
                            resultCommands.Add(commandAvailableList[j]);
                        }
                        break;
                    }

                    if (!resultCommands.Contains(commandAvailableList[j]))
                    {
                        break;
                    }

                    if (resultCommands.Count != 0)
                    {
                        if (resultCommands.Last().GetType().GetProperty("Value") != null)
                        {
                            Type commandWithValue = resultCommands.Last().GetType();
                            PropertyInfo property = commandWithValue.GetProperty("Value");
                            Type propertyType = property.PropertyType;

                            try
                            {
                                Convert.ChangeType(args[i], propertyType);
                            }
                            catch (Exception)
                            {
                                wasError = true;
                                break;
                            }
                            property.SetValue(resultCommands.Last(), Convert.ChangeType(args[i], propertyType));
                            break;
                        }
                    }
                }
                if (i == args.Length)
                {
                    wasError = true;
                }
            }

            if (i < args.Length)
            {
                if (resultCommands.Count != 0)
                {
                    if (resultCommands.Last().GetType().GetProperty("Value") != null)
                    {
                        Type commandWithValue = resultCommands.Last().GetType();
                        PropertyInfo property = commandWithValue.GetProperty("Value");
                        Type propertyType = property.PropertyType;

                        try
                        {
                            Convert.ChangeType(args[i], propertyType);
                        }
                        catch (Exception)
                        {
                            wasError = true;
                        }
                        property.SetValue(resultCommands.Last(), Convert.ChangeType(args[i], propertyType));
                    }
                }
            }

            if ((wasError == true) | (resultCommands.Count == 0))
            {
                return new List<ICommand>()
                    {
                        new ErrorCommand()
                    };
            }
            return resultCommands;
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

        public Parser()
        {

        }
    }
}
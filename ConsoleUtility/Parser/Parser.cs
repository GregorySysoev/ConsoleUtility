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
            for (int i = 0, j = 0; j < commandAvailableList.Count & !wasError; j++)
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

                    if (resultCommands.Count != 0)
                    {
                        if (resultCommands.Last().GetType().GetProperty("Value") != null)
                        {
                            Type commandWithValue = resultCommands.Last().GetType();
                            PropertyInfo property = commandWithValue.GetProperty("Value");
                            Type propertyType = property.PropertyType;

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
            if ((wasError == true) | (resultCommands.Count == 0))
            {
                return new List<ICommand>()
                    {
                        new ErrorCommand()
                    };
            }
            return resultCommands;
            // bool wasError = false;
            // for (int i = 0, j = 0; i < args.Length & !wasError; i++)
            // {
            //     for (j = 0; j < commandAvailableList.Count; j++)
            //     {
            //         if (commandAvailableList[j].GetType().GetCustomAttribute<CommandPrefixAttribute>() == null)
            //         {
            //             continue;
            //         }

            //         bool commandDecided = commandAvailableList[j]
            //             .GetType()
            //             .GetCustomAttribute<CommandPrefixAttribute>()
            //             .prefix
            //             .Contains(args[i]);

            //         if (commandDecided)
            //         {
            //             if (!resultCommands.Contains(commandAvailableList[j]))
            //             {
            //                 resultCommands.Add(commandAvailableList[j]);
            //             }
            //             break;
            //         }
            //         if ((resultCommands.Count == 0))
            //         {
            //             continue;
            //         }

            //         if (resultCommands.Last().GetType().GetProperty("Value") == null)
            //         {
            //             continue;
            //         }
            //         else
            //         {
            //             Type commandWithValue = resultCommands.Last().GetType();
            //             PropertyInfo oProp = commandWithValue.GetProperty("Value");
            //             Type tProp = oProp.PropertyType;

            //             oProp.SetValue(resultCommands.Last(), Convert.ChangeType(args[i], tProp));
            //             commandWithValue.GetProperty("Value")
            //             .SetValue(resultCommands.Last(), args);
            //             break;
            //         }
            //     }
            //     if (j == commandAvailableList.Count)
            //     {
            //         wasError = true;
            //     }
            // }


            // if (wasError)
            // {
            //     resultCommands.Add(new ErrorCommand());
            // }
            // return resultCommands;
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
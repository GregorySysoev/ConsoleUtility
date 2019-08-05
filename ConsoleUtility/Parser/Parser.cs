using System;
using System.Collections;
using ConsoleUtility;
using System.Collections.Generic;

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
            commands = IdentifyCommands(args);
            return commands;
        }
        public Parser()
        {

        }

        public List<ICommand> IdentifyCommands(string[] args)
        {
            List<ICommand> result = new List<ICommand>();

            var assembly = typeof(ICommand).Assembly;
            var classes = assembly.GetTypes();

            for (int j = 0; j < args.Length; j++)
            {
                for (int i = 0; i < classes.Length; i++)
                {
                    if (classes[i].Equals(args[j])) //
                    {
                        result.Add((ICommand)classes[i]);
                    }
                }
            }
            return result;
        }
    }
}
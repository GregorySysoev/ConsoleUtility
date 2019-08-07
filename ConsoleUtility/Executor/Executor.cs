using System;
using ConsoleUtility;
using System.Collections.Generic;
using System.IO;

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
            int countOffThreads = 1;
            string stringToSearch = "";
            string path = System.Environment.CurrentDirectory;

            bool help = false;
            bool error = false;

            for (int i = 0; i < _commands.Count && !error && !help; i++)
            {
                switch (_commands[i])
                {
                    case ErrorCommand errorCommand:
                        error = true;
                        break;
                    case HelpCommand helpCommand:
                        help = true;
                        break;
                    case ThreadSelectCommand threadCount:
                        _commands.Remove(threadCount);
                        countOffThreads = threadCount.Value;
                        break;
                    case SearchCommand search:
                        _commands.Remove(search);
                        stringToSearch = search.Value;
                        break;
                    default:
                        continue;
                }
            }

            if (stringToSearch == "")
            {
                return;
            } //error
            //TODO решить что делать с help и error

            _commands.Add(new LineFoundSearchCommand());
            _commands.Add(new TimeCommand());

            Finder finder = new Finder(stringToSearch);
        }
    }
}
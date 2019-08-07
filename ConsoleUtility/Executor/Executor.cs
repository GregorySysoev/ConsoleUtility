using System;
using ConsoleUtility;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
            int countOfThreads = 1;
            string stringToSearch = "";
            string pathToFind = System.Environment.CurrentDirectory;

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
                        countOfThreads = threadCount.Value;
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
            }
            if (error)
            {
                return;
            }
            if (help)
            {
                return;
            }

            //TODO решить что делать с help и error

            _commands.Add(new LineFoundSearchCommand());
            _commands.Add(new TimeCommand());
            _commands.Add(new PathToFileCommand());

            Finder finder = new Finder(stringToSearch, pathToFind, _commands, countOfThreads);
        }
    }
}
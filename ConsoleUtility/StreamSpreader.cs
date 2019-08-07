using System;
using ConsoleUtility;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleUtility
{

    public class StreamSpreader
    {
        private string _stringToSearch;
        public string _pathToFind;
        private List<ICommand> _commands;
        private Thread[] _threads;

        public StreamSpreader(string stringToSearch,
                        string pathToFind,
                        List<ICommand> commands,
                        int countOfThreads)
        {
            _stringToSearch = stringToSearch;
            _pathToFind = pathToFind;
            _commands = commands;
            _threads = new Thread[countOfThreads];
        }
    }
}
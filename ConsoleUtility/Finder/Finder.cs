using ConsoleUtility;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using myTree;

namespace ConsoleUtility
{
    public class Finder
    {
        private string _stringToSearch;
        private List<String> _pathToFilesList;
        private myTree.FileWriter _filesList;
        private List<ICommand> _commands;
        private int _countOfThreads;
        private string _pathToFind;
        public void getPathToFilesList()
        {
            _filesList = new myTree.FileWriter();
            Algorithm algorithm = new Algorithm(new string[] { }, _filesList, _pathToFind);
            algorithm.Execute();
        }
        public string findStringInFile(string filePath)
        {
            Thread t = Thread.CurrentThread;
            string pathToFile;
            _filesList.listOfFilesConcurentQueue.TryDequeue(out pathToFile);
            return "";
        }
        public Finder(string stringToSearch,
                        string pathToFind,
                        List<ICommand> commands,
                        int countOfThreads)
        {
            _pathToFind = pathToFind;
            _countOfThreads = countOfThreads;
            _commands = commands;
            _stringToSearch = stringToSearch;
        }


        public void Find(string pathToFile, List<ICommand> commands)
        {
            Thread threadThatFindFiles = new Thread(new ThreadStart(getPathToFilesList));
            Thread[] threadsThatFindStringInFiles = new Thread[_countOfThreads];
            for (int i = 0; i < _countOfThreads; i++)
            {
            }
        }
    }
}
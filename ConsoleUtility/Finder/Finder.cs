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
        public void GetPathToFilesList()
        {
            _filesList = new myTree.FileWriter();
            Algorithm algorithm = new Algorithm(new string[] { }, _filesList, _pathToFind);
            algorithm.Execute();
        }
        public List<int> FindStringInFile(string filePath)
        {
            StreamReader file = new StreamReader(filePath);
            int line = 1;
            string currentString = "";
            List<int> result = new List<int>();
            while ((currentString = file.ReadLine()) != null)
            {
                if (currentString.Contains(_stringToSearch))
                {
                    result.Add(line);
                }
                line++;
            }
            // Thread t = Thread.CurrentThread;
            // string pathToFile;
            // _filesList.listOfFilesConcurentQueue.TryDequeue(out pathToFile);

            return result;
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
            GetPathToFilesList();
        }


        public void FindAndPrint(IPrinter printer)
        {
            // Thread threadThatFindFiles = new Thread(new ThreadStart(GetPathToFilesList));
            // Thread[] threadsThatFindStringInFiles = new Thread[_countOfThreads];
            // for (int i = 0; i < _countOfThreads; i++)
            // {
            // }
            foreach (var file in _filesList.listOfFilesConcurentQueue)
            {
                if (FindStringInFile(file) is List<int> result)
                {
                    foreach (var res in result)
                    {
                        printer.Print(file + " " + res.ToString());
                    }
                }
            }
        }
    }
}
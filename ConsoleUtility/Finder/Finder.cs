using ConsoleUtility;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using myTree;

namespace ConsoleUtility
{
    public class Finder
    {
        IPrinter _printer;
        private bool _filesEnds = true;
        private string _stringToSearch;
        private myTree.FileWriter _filesList;
        private List<ICommand> _commands;
        private int _countOfThreads;
        private string _pathToFind;
        public void GetPathToFilesList()
        {
            _filesList = new myTree.FileWriter();
            Algorithm algorithm = new Algorithm(new string[] { }, _filesList, _pathToFind);
            algorithm.Execute();
            _filesEnds = false;
        }
        public List<int> FindStringInFile(string filePath)
        {
            StreamReader file;
            try
            {
                file = new StreamReader(filePath);
            }
            catch
            {
                return null;
            }

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
            return result;
        }
        public Finder(string stringToSearch,
                        string pathToFind,
                        List<ICommand> commands,
                        int countOfThreads,
                        IPrinter printer)
        {
            _pathToFind = pathToFind;
            _countOfThreads = countOfThreads;
            _commands = commands;
            _stringToSearch = stringToSearch;
            _printer = printer;
        }


        public void FindAndPrint()
        {
            while (_filesList.listOfFilesConcurentQueue.TryDequeue(out string filePath) || _filesEnds)
            {
                if ((FindStringInFile(filePath) is List<int> result))
                {
                    foreach (var res in result)
                    {
                        _printer.Print(filePath + " " + res.ToString());
                    }
                }
            }
        }

        public void Find()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < _countOfThreads; i++)
            {
                tasks.Add(Task.Factory.StartNew(FindAndPrint));
            }
            var taskGetFiles = Task.Factory.StartNew(GetPathToFilesList);
            tasks.Add(taskGetFiles);
            Task.WaitAll(tasks.ToArray());
        }
    }
}
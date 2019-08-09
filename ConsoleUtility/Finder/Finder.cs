using ConsoleUtility;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using myTree;
using System.Diagnostics;

namespace ConsoleUtility
{
    public class Finder
    {
        private IPrinter _printer;
        private int _countOfThreads;
        private string _stringToSearch;
        private List<ICommand> _commands;
        private bool _filesEnds = false;
        private ConcurrentQueue<string> _infoToPrint;
        private myTree.FileWriter _filesList;
        private string _pathToFind;
        public void GetPathToFilesList()
        {
            Algorithm algorithm = new Algorithm(new string[] { }, _filesList, _pathToFind);
            algorithm.Execute();
            _filesEnds = true;
        }
        public Dictionary<int, long> FindStringInFile(string filePath)
        {
            long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            StreamReader file;
            try
            {
                file = new StreamReader(filePath);
            }
            catch
            {
                return null;
            }
            var sw = new Stopwatch();
            int line = 1;
            string currentString = "";
            Dictionary<int, long> result = new Dictionary<int, long>();
            sw.Start();
            while ((currentString = file.ReadLine()) != null)
            {
                if (currentString.Contains(_stringToSearch))
                {
                    result.Add(line, nanosecPerTick * sw.ElapsedTicks);
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
            _filesList = new FileWriter();
            _infoToPrint = new ConcurrentQueue<string>();
            _pathToFind = pathToFind;
            _countOfThreads = countOfThreads;
            _commands = commands;
            _stringToSearch = stringToSearch;
            _printer = printer;
        }

        public void FindAndPrint()
        {
            while (_filesEnds)
            {
                while (_filesList.listOfFilesConcurentQueue.TryDequeue(out string filePath))
                {
                    if ((FindStringInFile(filePath) is Dictionary<int, long> result))
                    {
                        foreach (var res in result)
                        {
                            _infoToPrint.Enqueue($"{filePath} line={res.Key} time={res.Value} thread={Thread.CurrentThread.ManagedThreadId}");
                        }
                    }
                }
            }
        }

        public void PrintInfo()
        {
            while (true)
            {
                if (_infoToPrint.TryDequeue(out string informationAboutSearch))
                {
                    _printer.Print(informationAboutSearch);
                }
            }
        }
        public void Find()
        {
            var producer = new Thread(GetPathToFilesList);
            producer.Start();
            var consumers = new Thread[_countOfThreads];
            for (int i = 0; i < _countOfThreads; i++)
            {
                consumers[i] = new Thread(FindAndPrint);
                consumers[i].Start();
            }
            var printer = new Thread(PrintInfo);
            printer.Start();
        }
    }
}
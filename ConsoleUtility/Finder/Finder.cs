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
                    sw.Stop();
                    result.Add(line, nanosecPerTick * sw.ElapsedTicks);
                    sw.Start();
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
            // Не потокобезопасный.
            while (_filesList.listOfFilesConcurentQueue.TryDequeue(out string filePath) || _filesEnds)
            {
                if ((FindStringInFile(filePath) is Dictionary<int, long> result))
                {
                    foreach (var res in result)
                    {
                        _printer.Print($"{filePath} line={res.Key} time={res.Value} thread={Thread.CurrentThread.ManagedThreadId}");
                    }
                }
            }
        }

        public void Find()
        {
            var producer = new Task(GetPathToFilesList);
            producer.Start();
            var consumers = new Thread[_countOfThreads];
            for (int i = 0; i < _countOfThreads; i++)
            {
                consumers[i] = new Thread(FindAndPrint);
                consumers[i].Start();
            }

            // var tasks = new List<Task>();
            // for (int i = 0; i < _countOfThreads; i++)
            // {
            //     tasks.Add(Task.Factory.StartNew(FindAndPrint));
            // }
            // var taskGetFiles = Task.Factory.StartNew(GetPathToFilesList);
            // tasks.Add(taskGetFiles);
            // Task.WaitAll(tasks.ToArray());
        }
    }
}
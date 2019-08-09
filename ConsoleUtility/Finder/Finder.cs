using ConsoleUtility;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using myTree;
using System.Diagnostics;
using System.Text;

namespace ConsoleUtility
{
    public struct InfoFromSearch
    {
        public int Line { get; }
        public long Time { get; }
        public string Path { get; }
        public int ThreadId { get; }
        public InfoFromSearch(int line, long time, string path, int threadId)
        {
            Line = line;
            Time = time;
            Path = path;
            ThreadId = threadId;
        }
    }
    public class Finder
    {
        private IPrinter _printer;
        private int _countOfThreads;
        private string _stringToSearch;
        private List<ICommand> _commands;
        private ConcurrentQueue<InfoFromSearch> _infoToPrint;
        private myTree.FileWriter _filesList;
        private string _pathToFind;
        public void GetPathToFilesList()
        {
            Algorithm algorithm = new Algorithm(new string[] { }, _filesList, _pathToFind);
            algorithm.Execute();
        }
        public void FindStringInFile(string filePath)
        {
            long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            StreamReader file;
            try
            {
                file = new StreamReader(filePath);
            }
            catch
            {
                return;
            }
            var sw = new Stopwatch();
            int line = 1;
            string currentString = "";
            sw.Start();
            while ((currentString = file.ReadLine()) != null)
            {
                if (currentString.Contains(_stringToSearch))
                {
                    _infoToPrint.Enqueue(new InfoFromSearch(
                        line,
                        nanosecPerTick * sw.ElapsedTicks,
                        filePath,
                        Thread.CurrentThread.ManagedThreadId
                    ));
                }
                line++;
            }
        }
        public Finder(string stringToSearch,
                        string pathToFind,
                        List<ICommand> commands,
                        int countOfThreads,
                        IPrinter printer)
        {
            _filesList = new FileWriter();
            _infoToPrint = new ConcurrentQueue<InfoFromSearch>();
            _pathToFind = pathToFind;
            _countOfThreads = countOfThreads;
            _commands = commands;
            _stringToSearch = stringToSearch;
            _printer = printer;
        }

        public void FindAndAfterPrint()
        {
            while (true)
            {
                if (_filesList.listOfFilesConcurentQueue.TryDequeue(out string filePath))
                {
                    FindStringInFile(filePath);
                }
            }
        }

        public void PrintInfo()
        {
            while (true)
            {
                if (_infoToPrint.TryDequeue(out InfoFromSearch res))
                {
                    StringBuilder str = new StringBuilder();
                    str.Append($"{res.Path.Replace(_pathToFind, "")} ");
                    str.Append($"line = {res.Line} ");
                    str.Append($"time = {res.Time} ");
                    str.Append($"thread = {res.ThreadId} ");
                    _printer.Print(str.ToString());
                }
            }
        }
        public void Find()
        {
            var producer = new Thread(GetPathToFilesList);
            producer.Start();
            var printer = new Thread(PrintInfo);
            printer.Start();
            var consumers = new Thread[_countOfThreads];
            for (int i = 0; i < _countOfThreads; i++)
            {
                consumers[i] = new Thread(FindAndAfterPrint);
                consumers[i].Start();
            }
        }
    }
}
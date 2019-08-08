using myTree;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace myTree
{
    public class FileWriter : IWriter
    {
        public ConcurrentQueue<string> listOfFilesConcurentQueue = new ConcurrentQueue<string>();

        public void Write(string text)
        {
            Console.WriteLine(new System.NotImplementedException().Message);
        }

        public async void Write(string prefix, FileSystemInfo info, string suffix)
        {
            if (info is FileInfo fInfo)
            {
                await Task.Run(() =>
                {
                    listOfFilesConcurentQueue.Enqueue(fInfo.FullName);
                });
            }
        }
    }
}
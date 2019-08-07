using System;
using System.IO;

namespace myTree
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }

        public void Write(string prefix, FileSystemInfo fInfo, string suffix)
        {
            Console.WriteLine(prefix + fInfo.Name + suffix);
        }
    }
}
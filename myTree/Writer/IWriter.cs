using System.IO;
using System;

namespace myTree
{
    public interface IWriter
    {
        void Write(string prefix, FileSystemInfo fInfo, string suffix);
        void Write(string text);
    }
}
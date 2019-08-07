using System;
using System.Collections.Generic;
using System.IO;

namespace myTree
{
    public class NameComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.Name.CompareTo(f2.Name);
        }
    }
}
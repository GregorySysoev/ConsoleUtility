using System;
using System.Collections.Generic;
using System.IO;

namespace myTree
{
    public class CreateTimeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.CreationTime.CompareTo(f2.CreationTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace myTree
{
    public class ChangeTimeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            return f1.LastWriteTime.CompareTo(f1.LastWriteTime);
        }
    }
}
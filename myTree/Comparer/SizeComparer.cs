using System;
using System.Collections.Generic;
using System.IO;

namespace myTree
{
    public class SizeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo f1, FileSystemInfo f2)
        {
            if ((f1 is DirectoryInfo) && (f2 is DirectoryInfo))
            {
                return 0;
            }
            else if (f1 is DirectoryInfo)
            {
                return -1;
            }
            else if (f2 is DirectoryInfo)
            {
                return 1;
            }
            else
            {
                FileInfo first = (FileInfo)f1;
                FileInfo second = (FileInfo)f2;
                return first.Length.CompareTo(second.Length);
            }
        }
    }
}
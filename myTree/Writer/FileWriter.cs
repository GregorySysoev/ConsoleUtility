using myTree;
using System;
using System.Collections.Generic;
using System.IO;

namespace myTree
{
    public class FileWriter : IWriter
    {
        private List<string> _listOfFiles = new List<string>();
        public void Write(string text)
        {
            throw new System.NotImplementedException();
        }

        public void Write(string prefix, FileSystemInfo info, string suffix)
        {
            if (info is FileInfo fInfo)
            {
                _listOfFiles.Add(fInfo.FullName);
            }
        }
    }
}
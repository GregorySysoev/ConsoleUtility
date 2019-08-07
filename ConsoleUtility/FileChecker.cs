using System;
using System.Collections.Generic;
using System.IO;
using ConsoleUtility;

namespace ConsoleUtility
{
    public class FileChecker
    {
        private Dictionary<FileInfo, bool> _fileChecked;

        public FileChecker(string pathToCurrentDirectory)
        {
            _fileChecked = new Dictionary<FileInfo, bool>();
            var filesCurrentDirectory = new DirectoryInfo(pathToCurrentDirectory).GetFiles();
            foreach (var file in filesCurrentDirectory)
            {
                _fileChecked.Add(file, false);
            }
        }

        public bool IsFileChecked(FileInfo f)
        {
            return _fileChecked[f];
        }

        public void CheckFile(FileInfo f)
        {
            _fileChecked[f] = true;
        }
    }
}
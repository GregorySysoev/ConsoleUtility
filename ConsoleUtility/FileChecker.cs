using System;
using System.Collections.Generic;
using System.IO;
using ConsoleUtility;

namespace ConsoleUtility
{
    public class FileChecker
    {
        private Dictionary<string, bool> _fileChecked;

        public FileChecker(ref List<string> listOfFilesInCatalog)
        {
            _fileChecked = new Dictionary<string, bool>();
            foreach (var file in listOfFilesInCatalog)
            {
                _fileChecked.Add(file, false);
            }
        }

        public bool IsFileChecked(string f)
        {
            return _fileChecked[f];
        }

        public void CheckFile(string f)
        {
            _fileChecked[f] = true;
        }
    }
}
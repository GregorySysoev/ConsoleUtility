using ConsoleUtility;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleUtility
{
    public class FindFilesInTree
    {
        private List<string> _listOfFiles = new List<string>();
        public FindFilesInTree(string currentDirectory)
        {

        }

        public void FindRecursive(string directoryPath)
        {
            DirectoryInfo currentFolder = new DirectoryInfo(directoryPath);
            var files = currentFolder.GetFiles();
            var directories = currentFolder.GetDirectories();

        }
    }
}
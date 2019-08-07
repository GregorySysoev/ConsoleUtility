using ConsoleUtility;
using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleUtility
{
    public class Finder
    {
        private string _stringToSearch;
        public Finder(string stringToSearch)
        {
            _stringToSearch = stringToSearch;
        }
        public void FindInFile()
        {

        }
        public async void Find(string pathToFile, List<ICommand> commands)
        {
            string currentString = "";
            using (StreamWriter fs = new StreamWriter(pathToFile))
            {
            }
        }
    }
}
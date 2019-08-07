using System.Collections.Generic;
using System;
using myTree;

namespace myTree
{
    public class Algorithm
    {
        Options options;
        private IWriter _writer;
        private string _path;
        public Algorithm(string[] args, IWriter writer, string path)
        {
            _writer = writer;
            Parser parser = new Parser();
            parser.Parse(args, out options);
            _path = path;
        }
        public void Execute()
        {
            Printer.Print(ref options, _writer, _path);
        }
    }
}
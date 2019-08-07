using System;
using System.Collections;
using System.Collections.Generic;
using myTree;

namespace myTree
{
    class Program
    {
        static void Main(string[] args)
        {
            IWriter writer = new ConsoleWriter();
            Algorithm algorithm = new Algorithm(args, writer, System.Environment.CurrentDirectory);
            algorithm.Execute();
        }
    }
}

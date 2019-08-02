using System;
using ConsoleUtility;

namespace ConsoleUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            manager.IdentifyCommand(args);
            manager.RunCommand();
        }
    }
}

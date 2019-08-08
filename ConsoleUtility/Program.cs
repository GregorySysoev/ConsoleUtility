using System;
using ConsoleUtility;

namespace ConsoleUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            IPrinter cp = new ConsolePrinter();
            Manager manager = new Manager(cp);
            manager.IdentifyCommand(args);
            manager.RunCommands();
        }
    }
}

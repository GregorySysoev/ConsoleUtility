using System;
using ConsoleUtility;

namespace ConsoleUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            IWriter cw = new ConsoleWriter();
            Manager manager = new Manager(cw);
            manager.IdentifyCommand(args);
            manager.RunCommands();
        }
    }
}

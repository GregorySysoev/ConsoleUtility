using ConsoleUtility;
using System.Collections.Generic;
namespace ConsoleUtility
{
    public class FindCommand : ICommand
    {
        public static List<string> availableArguments = new List<string>()
        {
            "-t",
            "-s"
        };

        public void Execute()
        {

        }
    }
}
using ConsoleUtility;
using System;
namespace ConsoleUtility
{
    public class TimeCommand : ICommand
    {
        public DateTime Value;
        public TimeCommand()
        {
            Value = new DateTime(0);
        }
    }
}
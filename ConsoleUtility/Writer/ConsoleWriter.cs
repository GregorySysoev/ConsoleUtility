using System;

namespace ConsoleUtility
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
using ConsoleUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtility
{
    public class StringPrinter : IPrinter
    {
        public StringBuilder str;
        public void Print(string text)
        {
            str.Append(text);
            str.Append("\n");
        }
    }
}
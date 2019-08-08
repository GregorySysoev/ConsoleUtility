using ConsoleUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUtility
{
    public class StringWriter : IWriter
    {
        public StringBuilder str;
        public void Write(string text)
        {
            str.Append(text);
            str.Append("\n");
        }
    }
}
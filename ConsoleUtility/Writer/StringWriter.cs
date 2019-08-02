using myTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace myTree
{
    public class StringWriter : IWriter
    {
        public StringBuilder str;
        public void Write(string text)
        {
            str.Append(text);
        }
        // public void WriteLine()
        // {
        //     str.Append("\n");
        // }

        // public void WriteLine(string text)
        // {
        //     str.Append(text);
        //     str.Append("\n");
        // }

        // public StringWriter()
        // {
        //     str = new StringBuilder();
        // }
    }
}
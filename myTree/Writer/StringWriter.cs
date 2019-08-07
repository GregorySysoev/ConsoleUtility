using myTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace myTree
{
    public class StringWriter : IWriter
    {
        public StringBuilder str;
        public void Write(string text)
        {
            str.Append(text);
            str.Append("\n");
        }

        public void Write(string prefix, FileSystemInfo fInfo, string suffix)
        {
            str.Append(prefix + fInfo.Name + suffix + "\n");
        }

        public StringWriter()
        {
            str = new StringBuilder();
        }
    }
}
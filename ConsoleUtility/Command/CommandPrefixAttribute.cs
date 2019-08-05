using System;
namespace ConsoleUtility
{
    [AttributeUsage(
        System.AttributeTargets.Class,
        AllowMultiple = true
    )]
    public class CommandPrefixAttribute : Attribute
    {
        public string prefix { get; set; }
        public CommandPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }

        public CommandPrefixAttribute(int arg)
        {

        }
    }


}
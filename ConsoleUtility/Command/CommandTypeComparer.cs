using ConsoleUtility;
using System;
using System.Collections.Generic;

namespace ConsoleUtility
{
    public class CommandTypeComparer : EqualityComparer<ICommand>
    {
        public override bool Equals(ICommand x, ICommand y)
        {
            return Type.Equals(x.GetType(), y.GetType());
        }

        public override int GetHashCode(ICommand x)
        {
            return x.GetType().GetHashCode();
        }
    }
}
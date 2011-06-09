using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public interface ICommandInterpreter
    {
        ICommand ReadCommand(string[] parts);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public interface ICommand
    {
        void SetArguments(string[] args);

        void Execute();
    }
}

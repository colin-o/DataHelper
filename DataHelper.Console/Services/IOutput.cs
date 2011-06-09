using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public interface IOutput
    {
        void Usage(string line);

        void Info(string line);

        void Warn(string line);

        void Fatal(string line);

        void Write(string line);
    }
}

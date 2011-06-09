using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    class StandardOutput : IOutput
    {
        public void Usage(string line)
        {
            System.Console.Out.WriteLine("Usage:\t{0}", line);
        }

        public void Info(string line)
        {
            System.Console.Out.WriteLine("Info:\t{0}", line);
        }

        public void Warn(string line)
        {
            System.Console.Out.WriteLine("Warn:\t{0}", line);
        }

        public void Fatal(string line)
        {
            System.Console.Out.WriteLine("Error:\t{0}", line);
        }

        public void Write(string line)
        {
            System.Console.Out.WriteLine(line);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommandInterpreter interpreter = new CommandInterpreter();
            ICommand command = interpreter.ReadCommand(args);
            IOutput output = new StandardOutput();

            command.Out = output;
            command.Execute();
        }
    }
}

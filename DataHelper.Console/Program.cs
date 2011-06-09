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
            ICommandInterpreter interpreter = Locator.Instance<ICommandInterpreter>();
            ICommand command = interpreter.ReadCommand(args);
            Locator.Inject(command);
            command.Execute();
        }
    }
}

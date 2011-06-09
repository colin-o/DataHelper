using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace DataHelper.Console
{
    public class ExampleUsageCommand : ICommand
    {
        private Dictionary<string, Type> _commandMap;

        [Inject]
        public IOutput Out { get; set; }

        public void SetArguments(string[] args) { }

        public void ExplainUsage() { }

        public ExampleUsageCommand(Dictionary<string, Type> commandMap)
        {
            _commandMap = commandMap;
        }

        public void Execute()
        {
            foreach (Type type in _commandMap.Values)
            {
                ICommand command = Activator.CreateInstance(type) as ICommand;
                command.Out = Out;
                command.ExplainUsage();

                Out.Write("");
                Out.Write("------------------------------------------------------------");
                Out.Write("");
            }
        }
    }
}

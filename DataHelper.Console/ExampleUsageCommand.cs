using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public class ExampleUsageCommand : ICommand
    {
        private IOutput _out;
        private Dictionary<string, Type> _commandMap;

        public IOutput Out
        {
            set { _out = value; }
        }

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
                command.Out = _out;
                command.ExplainUsage();

                _out.Write("");
                _out.Write("------------------------------------------------------------");
                _out.Write("");
            }
        }
    }
}

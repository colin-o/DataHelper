using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private static Dictionary<string, Type> _commandMap;

        static CommandInterpreter()
        {
            _commandMap = new Dictionary<string, Type>();
            _commandMap.Add("pullschema", typeof(PullSchemaCommand));
            _commandMap.Add("pulldata", typeof(PullDataCommand));
        }

        public ICommand ReadCommand(string[] parts)
        {
            if (parts == null || parts.Length == 0)
                return new ExampleUsageCommand(_commandMap);

            string commandName = parts[0];
            string[] arguments = parts.Skip(1).ToArray();
            Type commandType = null;

            if (_commandMap.ContainsKey(commandName))
                commandType = _commandMap[commandName];

            if (commandType == null)
                return null;

            ICommand command = Activator.CreateInstance(commandType) as ICommand;

            command.SetArguments(arguments);
            return command;
        }
    }
}

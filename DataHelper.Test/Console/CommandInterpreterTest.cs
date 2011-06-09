using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataHelper.Console;

namespace DataHelper.Test.Console
{
    [TestClass]
    public class CommandInterpreterTest
    {
        [TestMethod]
        public void Should_Recognize_Pull_Schema_Command_With_Server_And_Database_Specified()
        {
            // Given that I have a command interpreter ready
            ICommandInterpreter interpreter = new CommandInterpreter();
            
            // When I send the "pullschema" command with a server and database name
            ICommand command = interpreter.ReadCommand(new string[] { "pullschema", Test.Settings.ServerName, Test.Settings.DatabaseName });

            // Then I should receive an instance of the PullSchema command
            Assert.IsInstanceOfType(command, typeof(PullSchemaCommand));
            PullSchemaCommand typedCommand = command as PullSchemaCommand;
            // And it should have the correct settings
            Assert.AreEqual(Settings.ServerName, typedCommand.ServerName);
            Assert.AreEqual(Settings.DatabaseName, typedCommand.DatabaseName);
        }

        [TestMethod]
        public void Should_Recognize_Pull_Data_Command_With_Server_And_Database_Specified()
        {
            // Given that I have a command interpreter ready
            ICommandInterpreter interpreter = new CommandInterpreter();
            
            // When I send the "pulldata" command with a server and database name
            ICommand command = interpreter.ReadCommand(new string[] { "pulldata", Test.Settings.ServerName, Test.Settings.DatabaseName });

            // Then I should receive an instance of the PullSchema command
            Assert.IsInstanceOfType(command, typeof(PullDataCommand));
            PullDataCommand typedCommand = command as PullDataCommand;
            // And it should have the correct settings
            Assert.AreEqual(Settings.ServerName, typedCommand.ServerName);
            Assert.AreEqual(Settings.DatabaseName, typedCommand.DatabaseName);
        }

        [TestMethod]
        public void Should_Handle_Empty_Command()
        {
            // Given that I have a command interpreter ready
            ICommandInterpreter interpreter = new CommandInterpreter();

            // When I send an empty command
            ICommand command = interpreter.ReadCommand(new string[] { });

            // Then I should receive the ExampleUsage command
            Assert.IsInstanceOfType(command, typeof(ExampleUsageCommand));
        }
    }
}

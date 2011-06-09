using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataHelper.Console;
using Moq;
using DataHelper.Core;

namespace DataHelper.Test.Console
{
    [TestClass]
    public class PullSchemaCommandTest
    {
        [TestMethod]
        public void Should_Pull_Schema_To_File()
        {
            // Given that I have a pull schema command
            PullSchemaCommand command = new PullSchemaCommand();
            var fileManager = new Mock<IFileManager>();
            var schemaGenerator = new Mock<ISchemaGenerator>();
            var output = new Mock<IOutput>();
            command.SchemaGenerator = schemaGenerator.Object;
            command.FileManager = fileManager.Object;
            command.Out = output.Object;
            // And it has been set up properly
            command.ServerName = "server";
            command.DatabaseName = "database";

            // When I execute the command
            command.Execute();

            // Then the schema script be saved to the correct location
            schemaGenerator.Verify(s => s.GenerateSchemaFor("server", "database"), Times.Once(), "Did not generate schema");
            fileManager.Verify(f => f.SaveFileToSchemaDirectory("schema.sql", It.IsAny<string>()), Times.Once(), "Did not try to save file");
            // And the user should be informed
            output.Verify(o => o.Info(It.IsAny<string>()), Times.AtLeastOnce(), "Info was not displayed");
        }

        [TestMethod]
        public void Should_Not_Pull_Schema_With_Bad_Arguments()
        {
            // Given that I have a pull schema command
            PullSchemaCommand command = new PullSchemaCommand();
            var fileManager = new Mock<IFileManager>();
            var schemaGenerator = new Mock<ISchemaGenerator>();
            var output = new Mock<IOutput>();
            command.SchemaGenerator = schemaGenerator.Object;
            command.FileManager = fileManager.Object;
            command.Out = output.Object;
            // And it has not been set up properly
            command.ServerName = null;
            command.DatabaseName = null;

            // When I execute the command
            command.Execute();

            // Then the schema should not be saved.
            schemaGenerator.Verify(s => s.GenerateSchemaFor("server", "database"), Times.Never(), "Did generate schema");
            fileManager.Verify(f => f.SaveFileToSchemaDirectory("schema.sql", It.IsAny<string>()), Times.Never(), "Did try to save file");
            // And the command's usage should be explained.
            output.Verify(o => o.Usage(It.IsAny<string>()), Times.AtLeastOnce(), "Usage was not explained");
        }
    }
}

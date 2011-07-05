using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelper.Core;
using Ninject;

namespace DataHelper.Console
{
    public class PullSchemaCommand : CommandBase
    {
        [Inject]
        public ISchemaManager SchemaGenerator { get; set; }

        [Inject]
        public IFileManager FileManager { get; set; }

        public override void Execute()
        {
            if (string.IsNullOrWhiteSpace(this.ServerName) || string.IsNullOrWhiteSpace(this.DatabaseName))
            {
                ExplainUsage();
                return;
            }

            // Generate the script
            string schema = this.SchemaGenerator.GenerateSchemaFor(this.ServerName, this.DatabaseName);

            // Change all references to the database to test database instead
            string testDatabaseName = TestDatabaseName();
            schema = schema.Replace(DatabaseName, testDatabaseName);

            // Write the file
            this.FileManager.SaveFileToSchemaDirectory("schema.sql", schema);

            // Show info
            string schemaPath = this.FileManager.SchemaDirectoryPath();
            Out.Info(string.Format("Schema for {0} was written to {1}\\schema.sql", this.DatabaseName, schemaPath));
        }

        public string TestDatabaseName()
        {
            string testDatabaseName = null;
            if (DatabaseName.EndsWith("_development"))
                testDatabaseName = DatabaseName.Replace("_development", "_test");
            else
                testDatabaseName = DatabaseName + "_test";

            return testDatabaseName;
        }

        public override void ExplainUsage()
        {
            Out.Usage("pullschema <server name> <database name>");
        }
    }
}

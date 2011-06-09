using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public class PullSchemaCommand : CommandBase
    {
        public override void Execute()
        {
            if (string.IsNullOrWhiteSpace(this.ServerName) || string.IsNullOrWhiteSpace(this.DatabaseName))
            {
                ExplainUsage();
                return;
            }

            string schema = this.SchemaGenerator.GenerateSchemaFor(this.ServerName, this.DatabaseName);
            this.FileManager.SaveFileToSchemaDirectory("schema.sql", schema);
            string schemaPath = this.FileManager.SchemaDirectoryPath();
            Out.Info(string.Format("Schema for {0} was written to {1}\\schema.sql", this.DatabaseName, schemaPath));
        }

        public Core.ISchemaGenerator SchemaGenerator { get; set; }

        public IFileManager FileManager { get; set; }

        public override void ExplainUsage()
        {
            Out.Usage("pullschema <server name> <database name>");
        }
    }
}

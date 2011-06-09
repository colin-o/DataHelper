using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace DataHelper.Core
{
    public class MsSqlDataDumper : IDataDumper
    {
        private string serverName;
        private string databaseName;
        private Scripter script;
        private Server server;
        private Database database;

        public MsSqlDataDumper()
        {
        }

        public List<InsertionScript> DumpTables(string serverName, string databaseName)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            server = new Server(serverName);
            database = server.Databases[databaseName];
            script = new Scripter(server);

            // Common options
            script.Options.SchemaQualify = false;
            script.Options.AllowSystemObjects = false;
            script.Options.ScriptData = true;
            script.Options.ScriptSchema = false;
            script.Options.ScriptBatchTerminator = true;

            List<InsertionScript> scripts = new List<InsertionScript>();
            foreach (Table table in database.Tables)
                scripts.Add(GetScriptForTable(table));

            return scripts;
        }

        public InsertionScript GetScriptForTable(Table table)
        {
            InsertionScript insertionScript = new InsertionScript();
            insertionScript.TableName = table.Name;

            var result = script.EnumScript(new Urn[] { table.Urn });
            var builder = new StringBuilder();
            foreach (string line in result)
                builder.AppendLine(line);

            insertionScript.Script = builder.ToString();
            return insertionScript;
        }
    }
}

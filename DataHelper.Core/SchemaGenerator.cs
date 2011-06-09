using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;

namespace DataHelper.Core
{
    public class SchemaGenerator : ISchemaGenerator
    {
        private Scripter script;
        private Server server;
        private Database database;
        private StringBuilder scriptBuilder;

        public SchemaGenerator()
        {
        }

        public string GenerateSchemaFor(string serverName, string databaseName)
        {
            server = new Server(serverName);
            database = server.Databases[databaseName];
            script = new Scripter(server);
            scriptBuilder = new StringBuilder();
            List<Urn> urns = new List<Urn>();

            // Common options
            //script.Options.WithDependencies = true;
            script.Options.SchemaQualify = false;
            script.Options.AllowSystemObjects = false;
            script.Options.DriAll = true;
            script.Options.IncludeDatabaseRoleMemberships = false;
            script.Options.ScriptData = false;
            script.Options.ScriptDrops = false;
            script.Options.ScriptSchema = true;
            script.Options.ScriptBatchTerminator = true;
            script.Options.Indexes = true;
            script.Options.DriAllConstraints = true;

            // Drop database
            script.Options.IncludeIfNotExists = true;
            script.Options.ScriptDrops = true;
            urns.Add(database.Urn);
            AppendScriptsForObjects(urns);
            script.Options.IncludeIfNotExists = false;
            script.Options.ScriptDrops = false;
            urns.Clear();

            // Create database and tables
            urns.Add(database.Urn);

            foreach (Table table in database.Tables)
                urns.Add(table.Urn);
            AppendScriptsForObjects(urns);

            return scriptBuilder.ToString();
        }

        private void AppendScriptsForObjects(List<Urn> urns)
        {
            Urn[] urnArray = urns.ToArray();
            var scriptParts = script.EnumScript(urnArray);

            foreach (var part in scriptParts)
            {
                scriptBuilder.AppendLine(part);
                scriptBuilder.AppendLine("GO");
            }
        }
    }
}

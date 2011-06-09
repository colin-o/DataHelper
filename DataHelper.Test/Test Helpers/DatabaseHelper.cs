using Microsoft.SqlServer.Management.Smo;

namespace DataHelper.Test
{
    class DatabaseHelper
    {
        internal void CreateDatabase(string serverName, string databaseName)
        {
            Server server = new Server(serverName);
            Database database = server.Databases[databaseName];
            if (database != null)
                server.KillDatabase(databaseName);
            database = new Database(server, databaseName);
            database.Create();
        }

        internal void CreateDatabase()
        {
            CreateDatabase(Test.Settings.ServerName, Test.Settings.DatabaseName);
        }
    }
}

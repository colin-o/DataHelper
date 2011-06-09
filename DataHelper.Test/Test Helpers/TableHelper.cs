using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;

namespace DataHelper.Test
{
    class TableHelper
    {
        private string serverName;
        private string databaseName;
        private Server server;
        private Database database;
        private List<Column> keyColumns;
        private Table table;

        public TableHelper()
            : this(Test.Settings.ServerName, Test.Settings.DatabaseName)
        {
        }

        public TableHelper(string serverName, string databaseName)
        {
            this.serverName = serverName;
            this.databaseName = databaseName;
            server = new Server(this.serverName);
            database = server.Databases[this.databaseName];
            this.keyColumns = new List<Column>();
        }

        public TableHelper CreateTable(string tableName, Action<ColumnHelper> columns)
        {
            this.table = new Table(database, tableName);

            ColumnHelper columnHelper = new ColumnHelper(table, this);
            columns(columnHelper);

            if (keyColumns.Count > 0)
                CreatePrimaryKey();

            table.Create();

            return this;
        }

        private void CreatePrimaryKey()
        {
            Index index = new Index(this.table, "PK_" + this.table.Name);
            foreach (var column in this.keyColumns)
            {
                IndexedColumn indexedColumn = new IndexedColumn(index, column.Name);
                index.IndexedColumns.Add(indexedColumn);
            }

            index.IndexKeyType = IndexKeyType.DriPrimaryKey;
            this.table.Indexes.Add(index);
        }

        public void AddColumn(Column column)
        {
            this.table.Columns.Add(column);
        }

        public void AddKeyColumn(Column keyColumn)
        {
            this.keyColumns.Add(keyColumn);
        }

        public TableHelper AddRow(params object[] parameterValues)
        {
            List<string> insertableColumns = new List<string>();

            foreach (Column column in this.table.Columns)
            {
                if (column.Identity)
                    continue;

                insertableColumns.Add(column.Name);
            }

            if (insertableColumns.Count != parameterValues.Count())
                throw new Exception("Parameter count must match the number of non-identity columns");

            var columnList = string.Join(", ", insertableColumns.ToArray());
            var parameterList = string.Join(", ", insertableColumns.Select(column => "@" + column).ToArray());

            StringBuilder command = new StringBuilder();
            command.AppendFormat("INSERT INTO {0} ({1}) VALUES ({2})", table.Name, columnList, parameterList);

            string connectionString = this.server.ConnectionContext.ConnectionString + ";Initial Catalog=" + this.databaseName;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(command.ToString());
                sqlCommand.Connection = connection;

                for (var i = 0; i < insertableColumns.Count(); i++)
                    sqlCommand.Parameters.AddWithValue("@" + insertableColumns[i], parameterValues[i]);

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }

            return this;
        }
    }
}

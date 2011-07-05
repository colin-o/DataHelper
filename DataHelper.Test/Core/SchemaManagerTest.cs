using System.Text.RegularExpressions;
using DataHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataHelper.Test
{
    [TestClass()]
    public class SchemaManagerTest
    {
        [TestMethod()]
        public void Should_Generate_Schema_For_Database()
        {
            // Given that I have a server name and database name
            string server = Test.Settings.ServerName;
            string database = Test.Settings.DatabaseName;
            // And the database has a table
            DatabaseHelper db = new DatabaseHelper();
            db.CreateDatabase();
            TableHelper table = new TableHelper();
            table.CreateTable("Table1", t =>
                {
                    t.column("Column1", ColumnType.Integer, key: true, nulls: false, identity: true);
                    t.column("Column2", ColumnType.String);
                });

            // When I generate a schema script
            ISchemaManager generator = new MsSqlSchemaManager();
            string schema = generator.GenerateSchemaFor(server, database);

            // Then I should have a script that drops the database
            StringAssert.Contains(schema, "DROP DATABASE ["+ database +"]");
            // And creates the database
            StringAssert.Contains(schema, "CREATE DATABASE ["+ database +"]");
            // And creates the table
            StringAssert.Contains(schema, "CREATE TABLE [Table1]");
            // With two columns
            StringAssert.Contains(schema, "[Column1] [int] IDENTITY(1,1) NOT NULL");
            StringAssert.Matches(schema, new Regex(@"\[Column2\] \[varchar\]\(50\) .* NULL"));
        }

        [TestMethod()]
        public void Should_Load_Schema_For_Database()
        {
            // Given that I have a server name and database name
            string server = Test.Settings.ServerName;
            string database = Test.Settings.DatabaseName;
            // And I have a schema script
            string schemaScript = null;
        }
    }
}

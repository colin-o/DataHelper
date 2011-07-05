using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataHelper.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataHelper.Test.Core
{
    [TestClass]
    public class DataManagerTest
    {
        [TestMethod]
        public void Should_Dump_Table_Data_To_Separate_Scripts()
        {
            // Given that I have a database with more than one table and each contains some data
            DatabaseHelper db = new DatabaseHelper();
            db.CreateDatabase();
            TableHelper table1 = new TableHelper();
            table1.CreateTable("Person", t =>
            {
                t.column("ID", ColumnType.Integer, true, false, true);
                t.column("Name", ColumnType.String);
                t.column("Age", ColumnType.Integer);
            })
            .AddRow("Colin", 28)
            .AddRow("Katherine", 25);

            TableHelper table2 = new TableHelper();
            table2.CreateTable("Animal", t =>
            {
                t.column("ID", ColumnType.Integer, true, false, true);
                t.column("Name", ColumnType.String);
                t.column("Kind", ColumnType.String);
            })
            .AddRow("Tiny", "Cat")
            .AddRow("Mayzie", "Dog");

            // When I dump the data
            IDataManager dumper = new MsSqlDataManager();
            List<InsertionScript> scripts = dumper.DumpTables(Test.Settings.ServerName, Test.Settings.DatabaseName);

            // I should get a data insertion script for each table
            var personScript = scripts.Where(s => s.TableName == "Person").FirstOrDefault();
            var animalScript = scripts.Where(s => s.TableName == "Animal").FirstOrDefault();
            Assert.IsNotNull(personScript);
            Assert.IsNotNull(animalScript);
            // And the scripts should have INSERT statements
            StringAssert.Matches(personScript.Script, new Regex("INSERT.*Person.*'Colin'.*28"));
            StringAssert.Matches(animalScript.Script, new Regex("INSERT.*Animal.*'Tiny'.*'Cat'"));
        }
    }
}

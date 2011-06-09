using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Core
{
    public interface IDataDumper
    {
        List<InsertionScript> DumpTables(string serverName, string databaseName);
    }
}

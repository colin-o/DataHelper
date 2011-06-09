using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace DataHelper.Test
{
    enum ColumnType
    {
        Integer, String
    }

    class ColumnHelper
    {
        private Table table;
        private TableHelper helper;

        public ColumnHelper(Table table, TableHelper helper)
        {
            this.table = table;
            this.helper = helper;
        }
        public void column(string name, ColumnType type, bool key = false, bool nulls = true, bool identity = false)
        {
            Column newColumn = new Column(table, name);
            newColumn.DataType = DataTypeForColumn(type);
            newColumn.Nullable = nulls;
            newColumn.Identity = identity;

            helper.AddColumn(newColumn);

            if (key)
                helper.AddKeyColumn(newColumn);
        }

        public DataType DataTypeForColumn(ColumnType type )
        {
            DataType colType = null;

            switch(type)
            {
                case ColumnType.Integer:
                    colType = DataType.Int;
                    break;

                case ColumnType.String:
                    colType = DataType.VarChar(50);
                    break;

                default:
                    colType = DataType.Int;
                    break;
            }

            return colType;
        }
    }
}

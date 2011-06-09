using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper.Console
{
    public interface IFileManager
    {
        string SchemaDirectoryPath();

        void SaveFileToSchemaDirectory(string name, string contents);
    }
}

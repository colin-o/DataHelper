using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataHelper.Console
{
    class StandardFileManager : IFileManager
    {
        public string SchemaDirectoryPath()
        {
            return Environment.CurrentDirectory;
        }

        public void SaveFileToSchemaDirectory(string name, string contents)
        {
            var filePath = Path.Combine(SchemaDirectoryPath(), name);
            using (StreamWriter writer = new StreamWriter(filePath))
                writer.Write(contents);
        }
    }
}

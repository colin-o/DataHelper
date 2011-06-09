
namespace DataHelper.Core
{
    public interface ISchemaGenerator
    {
        string GenerateSchemaFor(string server, string database);
    }
}

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtIQueryable
{
    private const decimal DecimalPrecision = 0.005m;

    public static IQueryable<T> DynamicSearch<T>(this IQueryable<T> query, Dictionary<string, string> search, List<ColumnMapping> columnMappings)
    {
        return null;
    }


    #region Helper
    // helper

    public class ColumnMapping
    {
        public string viewColumnName { get; set; }
        public string dbColumnName { get; set; }
        // public EnumDb
    }
    #endregion
}
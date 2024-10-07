using System.Linq;
using System.Linq.Dynamic.Core;
// using System.Linq.Dynamic.Core;
using databasepmapilearn6.Enumerations;

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtIQueryable
{
    private const decimal DecimalPrecision = 0.005m;

    /// Search per kolom.
    public static IQueryable<T> DynamicSearch<T>(this IQueryable<T> query, Dictionary<string, string> search, List<ColumnMapping> columnMappings)
    {
        // pastikan search tidak kosong 
        if (search?.Count <= 0) return query;

        // konstruksi pasangan nama kolom, search value, dan tipe kolom yang diinput user
        var columnMappedSearch = new List<ColumnMappedSearch>(search.Select(m =>
        {
            // ambil nama kolom dari list komplit kolom
            var selectedTuple = columnMappings.First(n => n.viewColumnName == m.Key);

            return ColumnMappedSearch.Create(
                dbColumnName: selectedTuple.dbColumnName,
                searchQuery: m.Value,
                dbDataType: selectedTuple.dbDataType
            );
        }));

        var nonEmptyColumnsMappedSearch = columnMappedSearch.Where(m => !string.IsNullOrEmpty(m.searchQuery) && !string.IsNullOrEmpty(m.dbColumnName));

        // jangan lakukan apa apa kalau user tidak melakukan search apapun
        if (!nonEmptyColumnsMappedSearch.Any()) return query;

        // konstruksi query untuk setiap kolom yang di search user
        foreach (var columnMapSearch in nonEmptyColumnsMappedSearch)
        {
            // ambil nama kolom
            string[] columnName = columnMapSearch.dbColumnName.Split(";");

            // pilih tipe data untuk kolom tersebut
            switch (columnMapSearch.dbDataType)
            {
                // tipe string
                case EnumDbdt.STRING:
                    if (columnName.Count() > 1)
                    {
                        // query = query.Where($"")
                        query = query.Where($"({columnName[0]}.ToLower()).Contains(\"{columnMapSearch.searchQuery.ToLower()}\") || ({columnName[1]}.ToLower()).Contains(\"{columnMapSearch.searchQuery.ToLower()}\")");
                    }
                    else
                    {
                        query = query.Where($"({columnName[0]}.ToLower()).Contains(\"{columnMapSearch.searchQuery.ToLower()}\")");
                    }
                    break;

                // tipe decimal
                case EnumDbdt.DECIMAL:
                    // konversi search string ke desimal
                    decimal searchValue = Convert.ToDecimal(columnMapSearch.searchQuery);
                    decimal lowerValue = searchValue - DecimalPrecision;
                    decimal higherValue = searchValue + DecimalPrecision;
                    query = query.Where($"m => @0 < m.{columnName} AND m.{columnName} <= @1", lowerValue, higherValue);
                    break;

                // tipe date
                case EnumDbdt.DATE:
                    string[] searcComponent = columnMapSearch.searchQuery.Split("-");

                    var tanggal = query.Select($"new (id as id ,{columnName[0]}.ToString(\"yyyy-MM-dd\") as date)").ToDynamicList();
                    var id = tanggal.Where(m => m.date == columnMapSearch.searchQuery).Select(m => m.id).ToDynamicList();

                    query = query.Where($"{id}.Contains(id.ToString())");
                    break;

                // tipe bool
                case EnumDbdt.BOOL:
                    break;

                // tipe int
                case EnumDbdt.INT:
                    int searchValueInt;
                    if (int.TryParse(columnMapSearch.searchQuery, out searchValueInt))
                    {
                        query = query.Where($"{columnName[0]}.ToString().Contains(\"{searchValueInt}\")");
                    }
                    break;

                // tipe int year
                case EnumDbdt.INTTOYEAR:
                    int searchNumber;
                    if (int.TryParse(columnMapSearch.searchQuery, out searchNumber))
                    {
                        query = query.Where("m => m.{columnName[0]}.Year.ToString().Contains(\"{searchNumber}\")");
                    }
                    break;
            }
        }

        // return combined query
        return query;
    }


    #region Helper
    // helper

    // mapping daftar colom yg bisa di search/sort
    // masih blm tau kegunaannya ??
    public class ColumnMapping
    {
        public string viewColumnName { get; set; }
        public string dbColumnName { get; set; }
        public EnumDbdt dbDataType { get; set; }


        // constructor
        private ColumnMapping()
        {
        }

        public static ColumnMapping Create(string viewColumnName, string dbColumnName, EnumDbdt dbDataType)
        {
            return new ColumnMapping
            {
                viewColumnName = viewColumnName,
                dbColumnName = dbColumnName,
                dbDataType = dbDataType,
            };
        }
    }


    // mapping kolom untuk search
    public class ColumnMappedSearch
    {
        public string dbColumnName { get; set; }
        public string searchQuery { get; set; }
        public EnumDbdt dbDataType { get; set; }

        // constructor
        public ColumnMappedSearch(string dbColumnName, string searchQuery, EnumDbdt dbDataType)
        {
            this.dbColumnName = dbColumnName;
            this.searchQuery = searchQuery;
            this.dbDataType = dbDataType;
        }

        public static ColumnMappedSearch Create(string dbColumnName, string searchQuery, EnumDbdt dbDataType)
        {
            return new ColumnMappedSearch(dbColumnName, searchQuery, dbDataType);
        }
    }

    #endregion
}
using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

/// Base input model used to provide table data.
/// The default filter parameters will be supplied by properties inside 'USER CLAIMS' region.    
public abstract class IMTable
{
    // Sorting per kolom => (nama kolom, order (true: asc, false: desc))
    public Dictionary<string, bool?> Sort { get; set; } = new Dictionary<string, bool?>();

    // Search per kolom => (nama kolom, search query)
    public Dictionary<string, string> Search { get; set; } = new Dictionary<string, string>();

    [Required(ErrorMessage = "Please provide page")]
    public int Page { get; set; }

    [Required(ErrorMessage = "Please provide number to show")]
    public int Show { get; set; }
}
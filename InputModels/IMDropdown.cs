using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

public abstract class IMDropdown
{
    // search query
    public string? Search { get; set; }

    // number of data to be displayed
    [Required(ErrorMessage = "jumlah data yang ditampilkan diperlukan")]
    public int Show { get; set; }

    /// List of ids that has been passed to the client before.
    /// Useful for editing data.
    public int[] AlreadyIds { get; set; } = { };

    public string[] AlreadyCodes { get; set; } = { };
}
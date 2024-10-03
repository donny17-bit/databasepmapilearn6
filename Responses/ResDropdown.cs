using databasepmapilearn6.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace databasepmapilearn6.Responses;

// cari tau cara baca ini
public class ResDropdown
{
    public IEnumerable<VMDropdown> data { get; set; }

    // constructor
    private ResDropdown(IEnumerable<VMDropdown> data)
    {
        if (data == null) this.data = new List<VMDropdown>(); else this.data = data;
    }

    public static ActionResult Success(IEnumerable<VMDropdown> data = null) => Res.Success(new ResDropdown(data));
}
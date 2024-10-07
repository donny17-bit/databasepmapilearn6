using Microsoft.AspNetCore.Mvc;

namespace databasepmapilearn6.Responses;


/// A helper class to provide structured response for table data.
/// The properties of this class reflects the payload on response body.  
public class ResTable
{
    // Dynamic data type to be sent.
    public object[] data { get; set; }

    /// Total eligible data based on filter (not the actual sent data).
    /// User for pagination in client-side.
    public int total_data { get; set; }

    public object others { get; set; }

    // constructor
    private ResTable(object[] data = null, int total_data = 0, object others = null)
    {
        this.data = data;
        this.total_data = total_data;
        this.others = others;
    }


    // success
    public static IActionResult Success(object[] data, int total_data, object others)
    {
        return Res.Success(new ResTable(data, total_data, others));
    }
}
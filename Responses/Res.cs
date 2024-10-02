using databasepmapilearn6.Constans;
using Microsoft.AspNetCore.Mvc;

namespace databasepmapilearn6.Responses;

public class Res
{
    public bool success { get; }
    public string version { get; }
    public string message { get; }
    public object payload { get; }
    // constructor    
    private Res(bool success, string message = null, object payload = null) { this.success = success; this.version = CVersion.APP; this.message = message; this.payload = payload; }


    public static IActionResult Success(object payload)
    {
        return new OkObjectResult(new Res(true, payload: payload));
    }
}
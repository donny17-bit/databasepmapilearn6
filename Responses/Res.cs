using databasepmapilearn6.Constans;
using databasepmapilearn6.Utilities;
using Microsoft.AspNetCore.Mvc;
using static databasepmapilearn6.ViewModels.VMAuth;

namespace databasepmapilearn6.Responses;

public class Res
{
    public bool success { get; }
    public string version { get; }
    public string message { get; }
    public object payload { get; }
    // constructor    
    private Res(bool success, string message = null, object payload = null) { this.success = success; this.version = CVersion.APP; this.message = message; this.payload = payload; }

    #region Success

    public static IActionResult Success(object payload)
    {
        return new OkObjectResult(new Res(true, payload: payload));
    }
    #endregion


    #region Failed

    public static IActionResult Failed(string message, Login vm)
    {
        return new BadRequestObjectResult(new Res(false, message: message, payload: vm));
    }

    public static IActionResult Failed(UtlLogger logger, Exception e)
    {
        // generate error code 
        string errorCode = UtlGenerator.ErrorCode();

        // log
        logger.Failed(e, errorCode);

        return new BadRequestObjectResult(new Res(false, message: $"error {errorCode}: {e.Message}"));
    }
    #endregion
}
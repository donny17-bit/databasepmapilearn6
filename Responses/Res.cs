using databasepmapilearn6.Constans;
using databasepmapilearn6.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static databasepmapilearn6.ViewModels.VMAuth;

namespace databasepmapilearn6.Responses;

public class Res
{
    public bool success { get; }
    public string version { get; }
    public string message { get; }
    public object payload { get; }

    #region constructor    
    private Res(bool success, string message = null, object payload = null) { this.success = success; this.version = CVersion.APP; this.message = message; this.payload = payload; }

    #endregion


    #region Success
    public static IActionResult Success() => new OkObjectResult(new Res(true));

    public static IActionResult Success(object payload)
    {
        return new OkObjectResult(new Res(true, payload: payload));
    }
    #endregion


    #region Failed
    public static IActionResult Failed(string message, object payload = null)
    {
        return new BadRequestObjectResult(new Res(false, message: message, payload: payload));
    }

    public static IActionResult Failed(string message, Login vmLogin)
    {
        return new BadRequestObjectResult(new Res(false, message: message, payload: vmLogin));
    }

    public static IActionResult Failed(UtlLogger logger, Exception e)
    {
        // generate error code 
        string errorCode = UtlGenerator.ErrorCode();

        // log
        logger.Failed(e, errorCode);

        return new BadRequestObjectResult(new Res(false, message: $"error {errorCode}: {e.Message}"));
    }

    public static IActionResult Failed(ModelStateDictionary modelState)
    {
        return new BadRequestObjectResult(new Res(false, message: "Input not valid", payload: modelState.SelectMany(dic => dic.Value.Errors).Select(modelError => modelError.ErrorMessage)));
    }
    #endregion


    #region not found
    public static IActionResult NotFound(string notFound) => new BadRequestObjectResult(new Res(false, message: $"{notFound} not found"));

    #endregion
}
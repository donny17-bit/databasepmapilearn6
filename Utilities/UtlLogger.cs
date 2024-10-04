using Serilog;

namespace databasepmapilearn6.Utilities;

public class UtlLogger
{
    // variable
    private readonly string _username;
    private readonly string _baseMessage;
    private readonly string _logId;

    // coba nanti getternya dihapus dan langsung ambil aja valuenya apa yg terjadi 
    // getter
    #region Getter

    public string GetUsername() => this._username;
    public string GetLogId() => this._logId;

    #endregion

    // contructor
    // UtlLogger for username is string 
    private UtlLogger(string Username, string BaseMessage, string AdditionalMessage, bool ShouldLogInitialized)
    {
        // make sure all parameter is provided
        if (string.IsNullOrEmpty(Username)) throw new ArgumentNullException(nameof(Username), "please provide username");
        if (string.IsNullOrEmpty(BaseMessage)) throw new ArgumentNullException(nameof(BaseMessage), "please provide base message");

        _username = Username;
        _baseMessage = BaseMessage;

        // create log id
        _logId = UtlGenerator.LogId();

        // log initialized (when needed)
        if (ShouldLogInitialized) Log.Information(CombinedMessage("initialized", AdditionalMessage));
    }

    // UtlLogger for logger is UtlLogger 
    // this is use to Create child instance from existing (parent) instance
    private UtlLogger(
        UtlLogger logger,
        string NewBaseMassage,
        string AdditionalMessage,
        bool ShouldLogInitialized
    )
    {
        // same username & log id
        this._username = logger.GetUsername();
        this._logId = logger.GetLogId();

        // update base message
        this._baseMessage = NewBaseMassage;

        if (ShouldLogInitialized) Log.Information(CombinedMessage("initilized (child)", AdditionalMessage));
    }


    #region Factories

    // create logger object
    public static UtlLogger Create(string Username, string BaseMessage, string AdditionalMessage = "", bool ShouldLogInitialized = true)
    {
        return new UtlLogger(Username, BaseMessage, AdditionalMessage, ShouldLogInitialized);
    }

    #region  don't know what is this !!!!! 
    #endregion
    // Create a new instance of this class using an existing instance with different base message but same log id.
    // This is useful when you want to do logging in classes other than the instance's source e.g. calling <see cref="UtlFile"/> method from a controller.
    // The created instance's <see cref="_username"/> and <see cref="_logId"/> will be the same.
    public static UtlLogger FromExisting(UtlLogger logger, string newBaseMassage, string AdditionalMessage = "", bool ShouldLogInitialized = true)
    {
        return new UtlLogger(logger, newBaseMassage, AdditionalMessage, ShouldLogInitialized);
    }

    #endregion


    // Success log
    #region Success

    public void Success(string additionalMessage = "")
    {
        Log.Information(CombinedMessage("success", additionalMessage));
    }

    #endregion


    #region Failed

    // Log failed operation because of catched exception (withour error code).
    // Mainly used when logging from a non-controller method.
    public void Failed(Exception e) => Log.Error(CombinedMessage("exception", GetInnerExceptionRecursive(e)));

    // Log failed operation because of catched exception.
    public void Failed(Exception e, string ErrorCode)
    {
        Log.Error(CombinedMessage($"exception ({ErrorCode})", GetInnerExceptionRecursive(e)));
    }

    // Log failed operation.
    public void Failed(string message)
    {
        Log.Warning(CombinedMessage("failed", message));
    }

    #endregion


    // helpers
    #region Helpers

    // displayed log in the console
    // Success format   : [username] -> "[log id] -> [base message] -> [process name] -> [additional message]"
    // Exception format : "[username] -> [log id] -> [base message] -> [process name] -> [exception message] -> inner -> [inner exception message] -> inner -> [...]" (will print all inner exceptions recursively)

    private string CombinedMessage(string processName, string additionalMessage)
    {
        return $"{_username} -> {_logId} -> {_baseMessage} -> {processName}" + (!string.IsNullOrEmpty(additionalMessage) ? $" -> {additionalMessage}" : string.Empty);
    }

    // get all inner exception
    private string GetInnerExceptionRecursive(Exception e)
    {
        // get inner exception inside exception recursively
        // and combine it 
        if (e.InnerException != null) return $"{e.Message} - inner -> {GetInnerExceptionRecursive(e.InnerException)}";

        // return it when there is no more inner exception
        return e.Message;
    }

    #endregion

}
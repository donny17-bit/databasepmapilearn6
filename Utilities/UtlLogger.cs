using Serilog;

namespace databasepmapilearn6.Utilities;

public class UtlLogger
{
    // variable
    private readonly string _username;
    private readonly string _baseMessage;
    private readonly string _logId;

    // contructor
    // UtlLogger additionalMessage is string 
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

    private string CombinedMessage(string processName, string additionalMessage)
    {
        return $"{_username} -> {_logId} -> {_baseMessage} -> {processName}" + (!string.IsNullOrEmpty(additionalMessage) ? $" -> {additionalMessage}" : string.Empty);
    }

    public static UtlLogger Create(string Username, string BaseMessage, string AdditionalMessage = "", bool ShouldLogInitialized = true
    )
    {
        return new UtlLogger(Username, BaseMessage, AdditionalMessage, ShouldLogInitialized);
    }
}
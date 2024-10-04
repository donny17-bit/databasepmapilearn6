using databasepmapilearn6.Configurations;
using databasepmapilearn6.InputModels;
using Microsoft.Extensions.Options;

namespace databasepmapilearn6.Utilities;

public class UtlEmail
{
    private readonly ConfEmail _confEmail;

    private readonly IHostEnvironment _hostingEnv;

    // constructor
    public UtlEmail(IOptions<ConfEmail> optEmail, IHostEnvironment hostingEnv)
    {
        _confEmail = optEmail.Value;
        _hostingEnv = hostingEnv;
    }

    public void Send(UtlLogger logger, IMEmail.Message message)
    {
        // check environment is production
        if (!_hostingEnv.IsProduction()) return;

        // email massage can't null
        if (message == null) return;

        // create log
        // UtlLogger logger1 = UtlLogger.
    }
}
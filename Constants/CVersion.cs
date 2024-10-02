using System.Reflection;

namespace databasepmapilearn6.Constans;

public static class CVersion
{
    public static string APP = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

    public static bool IS_DEVELOPMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
}
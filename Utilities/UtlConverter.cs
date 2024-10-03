using Newtonsoft.Json;

namespace databasepmapilearn6.Utilities;

public class UtlConverter
{
    public static string ObjectToJson(object input)
    {
        return JsonConvert.SerializeObject(input);
    }

}
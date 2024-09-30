using System.Security.Claims;

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtClaim
{
    // convert to claim to int from user claim
    private static int ClaimToInt(this IEnumerable<Claim> userClaim, string type) => Convert.ToInt32(userClaim.First(m => m.Type == type).Value);
    
    // get the string value from user claim
    private static string ClaimToString(this IEnumerable<Claim> userClaim, string type) => userClaim.First(m => m.Type == type).Value; 
    
    
    // get the properties from claim
    public static int GetUserId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("id");
    public static string GetUsername(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("username");
    public static string GetName(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("name");
    public static string GetEmail(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("email");
    public static int GetRoleId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("role_id");
    public static int GetPositionId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("position_id");
}
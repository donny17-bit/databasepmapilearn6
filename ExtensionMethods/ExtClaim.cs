using System.Security.Claims;

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtClaim
{
    // convert to claim to int from user claim
    private static int ClaimToInt(this IEnumerable<Claim> userClaim, string type) => Convert.ToInt32(userClaim.First(m => m.Type == type).Value);
    
    // get the string value from user claim
    private static string ClaimToString(this IEnumerable<Claim> userClaim, string type) => userClaim.First(m => m.Type == type).Value; 
    
    
    // get the properties from claim
    public static int GetUserId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("Id");
    public static string GetUsername(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("Username");
    public static string GetName(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("Name");
    public static string GetEmail(this IEnumerable<Claim> userClaim) => userClaim.ClaimToString("Email");
    public static int GetRoleId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("RoleId");
    public static int GetPositionId(this IEnumerable<Claim> userClaim) => userClaim.ClaimToInt("PositionId");
}
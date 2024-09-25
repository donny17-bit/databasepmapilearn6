using System.Security.Claims;

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtClaim
{
    // convert to claim to int
    private static int ClaimToInt(this IEnumerable<Claim> userClaim, string type) 
    {
        return Convert.ToInt32(userClaim.First(m => m.Type == type).Value);
    }

    // get the User Id from claim
    public static int GetUserId(this IEnumerable<Claim> userClaim) 
    {
        return userClaim.ClaimToInt("id");
    }
}
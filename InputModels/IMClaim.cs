using System.Security.Claims;
using databasepmapilearn6.ExtensionMethods;

namespace databasepmapilearn6.InputModels;

public class IMClaim
{
    public int Id {get; set;}
    public string Username {get; set;} = null!;
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public int RoleId {get; set;}
    public int PositionId {get; set;}

    // private constractor
    private IMClaim() {}

    public static IMClaim FromUserClaim(IEnumerable<Claim> userClaims) 
    {
        return new IMClaim {
            Id = userClaims.GetUserId()
        };
    }
}
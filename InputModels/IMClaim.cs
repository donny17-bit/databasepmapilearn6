using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using databasepmapilearn6.ExtensionMethods;
using databasepmapilearn6.models;

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
            Id = userClaims.GetUserId(),
            Username = userClaims.GetUsername(),
            Name = userClaims.GetName(),
            Email = userClaims.GetEmail(),
            RoleId = userClaims.GetRoleId(),
            PositionId = userClaims.GetPositionId()
        };
    }

    
    public static IMClaim FromDb(MUser userClaims) 
    {
        return new IMClaim {
            Id = userClaims.Id,
            Username = userClaims.Username,
            Name = userClaims.Name,
            Email = userClaims.Email,
            RoleId = userClaims.RoleId,
            PositionId = userClaims.PositionId
        };
    }

    public Claim[] ToClaim() {
        return new Claim[] {
            new(JwtRegisteredClaimNames.UniqueName, Username),
            new(ClaimTypes.Role, RoleId.ToString()),
        };
    }
}
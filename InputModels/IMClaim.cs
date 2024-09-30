using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using databasepmapilearn6.Constans;
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

    // ntar cari tahu cara kerjanya kek mana
    public Claim[] ToClaim() {
        return new Claim[] {
            new(CClaim.Id, Id.ToString()),
            new(CClaim.Username, Username),
            new(JwtRegisteredClaimNames.UniqueName, Username),
            new(CClaim.Name, Name),
            new(CClaim.Email, Email ?? ""),
            new(ClaimTypes.Role, RoleId.ToString()),
            new(CClaim.RoleId, RoleId.ToString()),
            new(CClaim.PositionId, PositionId.ToString()),
        };
    }
}
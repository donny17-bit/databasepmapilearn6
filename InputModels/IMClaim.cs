using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using databasepmapilearn6.Constans;
using databasepmapilearn6.ExtensionMethods;
using databasepmapilearn6.models;

namespace databasepmapilearn6.InputModels;

public class IMClaim
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public int RoleId { get; set; }
    public int PositionId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    // private constractor
    private IMClaim() { }

    public static IMClaim FromUserClaim(IEnumerable<Claim> userClaims)
    {
        return new IMClaim
        {
            Id = userClaims.GetUserId(),
            Username = userClaims.GetUsername(),
            RoleId = userClaims.GetRoleId(),
            PositionId = userClaims.GetPositionId(),
            Name = userClaims.GetName(),
            Email = userClaims.GetEmail()
        };
        // note 
        // parameter name on ExtClaim must be same as CClaim
        // include uppercase and lowercase letter
    }


    public static IMClaim FromDb(MUser userClaims)
    {
        return new IMClaim
        {
            Id = userClaims.Id,
            Username = userClaims.Username,
            RoleId = userClaims.RoleId,
            PositionId = userClaims.PositionId,
            Name = userClaims.Name,
            Email = userClaims.Email
        };
    }

    // ntar cari tahu cara kerjanya kek mana
    public Claim[] ToClaim()
    {
        return new Claim[] {
            new(JwtRegisteredClaimNames.UniqueName, Username),
            new(ClaimTypes.Role, RoleId.ToString()),

            new(CClaim.Id, Id.ToString()),
            new(CClaim.Username, Username),
            new(CClaim.RoleId, RoleId.ToString() ?? "-999"),
            new(CClaim.PositionId, PositionId.ToString()),
            new(CClaim.Name, Name),
            new(CClaim.Email, Email ?? ""),
        };
    }
}
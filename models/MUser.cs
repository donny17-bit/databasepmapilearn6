using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace databasepmapilearn6.models;

[Table("m_user")]
public class MUser
{
    public int Id {get; set;}

    [Column("role_id")]
    public int RoleId {get; set;}

    [Column("position_id")]
    public int PositionId {get; set;}

    public string Username {get; set;} = null!;
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;

    [Column("refresh_token")]
    public string? RefreshToken {get; set;}
    
    [Column("retry_count")]
    public int? RetryCount {get; set;}

    [Column("locked_until")]
    public DateTime? LockedUntil {get; set;}

    [Column("created_by")]
    public int CreatedBy {get; set;}

    [Column("created_date")]
    public DateTime CreatedDate {get; set;}

    [Column("updated_by")]
    public int? UpdatedBy {get; set;}

    [Column("updated_date")]
    public DateTime? UpdatedDate {get; set;}

    [Column("is_deleted")]
    public bool IsDeleted {get; set;}
}
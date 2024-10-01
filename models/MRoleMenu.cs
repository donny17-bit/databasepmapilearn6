using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;

namespace databasepmapilearn6.models;

[Table("m_role_menu")]
public class MRoleMenu
{
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("menu_id")]
    public int MenuId { get; set; }

    public virtual MMenu Menu { get; set; } = null!;
    public virtual MRole Role { get; set; } = null!;
}
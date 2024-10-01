using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace databasepmapilearn6.models;

[Table("m_role")]
public class MRole
{
    // constructor
    public MRole()
    {
        // this will auto create virtual properties to have object (not null)
        Users = new HashSet<MUser>();
        RoleMenus = new HashSet<MRoleMenu>();
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_date")]
    public DateTime? UpdatedDate { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public virtual ICollection<MUser> Users { get; set; }
    public virtual ICollection<MRoleMenu> RoleMenus { get; set; }
}
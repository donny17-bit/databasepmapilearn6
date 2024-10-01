using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_menu")]
public class MMenu
{
    public int ID { get; set; }

    [Column("icon_id")]
    public int IconId { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;
    public string? Component { get; set; }
    public string? Path { get; set; }
    public int Order { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public virtual MIcon Icon { get; set; } = null!;
}
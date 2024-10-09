using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_approval_detail")]
public class MApprovalDetail
{
    public int Id { get; set; }

    [Column("approval_id")]
    public int ApprovalId { get; set; }

    [Column("position_id")]
    public int PositionId { get; set; }

    public string Description { get; set; } = null!;
    public int Level { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    public virtual MApproval mApproval { get; set; }

    public virtual MPosition mPosition { get; set; }
}
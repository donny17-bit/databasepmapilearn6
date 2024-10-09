using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_approval")]
public class MApproval
{
    public int Id { get; set; }

    [Column("trx_type_id")]
    public int TrxTypeId { get; set; }

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

    // public virtual MTrxType mTrxType { get; set; }

}
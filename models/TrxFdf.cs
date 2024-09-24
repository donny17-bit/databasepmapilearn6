using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_fdf")]
public class TrxFdf
{    
    public int Id {get; set;}

    [Column("trx_type_id")]
    public int TrxTypeId {get; set;}
    
    [Column("trx_status_id")]
    public int TrxStatusId {get; set;}

    [Column("job_file_id")]
    public int JobFileId {get; set;}

    [Column("user_id")]
    public int UserId {get; set;}

    [Column("fdf_number")]
    public string FdfNumber {get; set;} = null!;

    [Column("approved_date")]
    public DateOnly? ApprovedDate {get; set;}

    [Column("expired_date")]
    public DateOnly? ExpiredDate {get; set;}

    [Column("approved_final_date")]
    public DateTime? ApprovedFinalDate {get; set;}

    public string? Description {get; set;}

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
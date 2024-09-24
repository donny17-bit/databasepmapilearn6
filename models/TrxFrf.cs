using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_frf")]
public class TrxFrf 
{
    public int Id {get; set;}

    [Column("trx_type_id")]
    public int TrxTypeId {get; set;}
    
    [Column("trx_status_id")]
    public int TrxStatusId {get; set;}
    
    [Column("frf_number")]
    public string FrfNumber {get; set;} = null!;

    [Column("pic_name")]
    public string PicName {get; set;} = null!;

    [Column("remove_reason")]
    public string RemoveReason {get; set;} = null!;

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
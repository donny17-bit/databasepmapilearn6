using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_fuf")]
public class TrxFuf 
{
    public int Id {get; set;}

    [Column("trx_type_id")]
    public int TrxTypeId {get; set;}
    
    [Column("trx_status_id")]
    public int TrxStatusId {get; set;}
    
    [Column("fuf_number")]
    public string FrfNumber {get; set;} = null!;

    [Column("pic_name")]
    public string PicName {get; set;} = null!;

    public int Year {get; set;}

    [Column("unit_id")]
    public int UnitId {get; set;}

    [Column("project_id")]
    public int ProjectId {get; set;}

    [Column("job_type_id")]
    public int JobTypeId {get; set;}

    [Column("file_type_id")]
    public int FileTypeId {get; set;}

    [Column("file_category_id")]
    public int FileCategoryId {get; set;}

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
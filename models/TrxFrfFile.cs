using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_frf_file")]
public class TrxFrfFile 
{
    public int Id {get; set;}

    [Column("frf_id")]
    public int FrfId {get; set;}
    
    [Column("job_file_id")]
    public int JobFileId {get; set;}
    
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
using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_fuf_file")]
public class TrxFufFile
{
    public int Id {get; set;}

    [Column("fuf_id")]
    public int FufId {get; set;}
    
    [Column("file_name")]
    public string FileName {get; set;} = null!;

    [Column("file_path")]
    public string FilePath {get; set;} = null!;

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
using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_file_type")]
public class MFileType
{
    public int Id {get; set;}
    public string name {get; set;} = null!;

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
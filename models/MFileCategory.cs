using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_file_category")]
public class MFileCategory 
{
    public int Id {get; set;}
    
    [Column("file_type_id")]
    public int FileTypeId {get; set;}

    public int Name {get; set;}

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
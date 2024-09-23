using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace databasepmapilearn6.models;

[Table("m_position")]
public class MPosition {
    public int Id {get; set;}
    public string Code{get; set;} = null!;
    public string Name{get; set;} = null!;
    public string Abbr{get; set;} = null!;
    
    [Column("created_by")]
    public int CreatedBy{get; set;}

    [Column("created_date")]
    public DateTime CreatedDate{get; set;}

    [Column("updated_by")]
    public int? UpdatedBy{get; set;}

    [Column("updated_date")]
    public DateTime? UpdatedDate {get; set;}

    [Column("is_deleted")]
    public bool IsDeleted{get; set;}
}
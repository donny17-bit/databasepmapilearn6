using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_icon")]
public class MIcon
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;
}
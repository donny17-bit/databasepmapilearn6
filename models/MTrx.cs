using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_trx")]
public class MTrx
{
    public int Id {get; set;}
    public string Name {get; set;} = null!;    
}
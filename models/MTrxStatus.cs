using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_trx_status")]
public class MTrxStatus
{
    public int Id {get; set;}

    [Column("trx_id")]
    public int TrxId {get; set;}
    public string Name {get; set;} = null!; 
    public string Description {get; set;} = null!;   
}
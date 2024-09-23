using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_trx_type")]
public class MTrxType
{
    public int Id {get; set;}

    [Column("trx_id")]
    public int TrxId {get; set;}

    [Column("trx_code")]
    public string TrxCode {get; set;} = null!;
    public string Name {get; set;} = null!; 
    public string Description {get; set;} = null!;   

    [Column("is_deleted")]
    public bool IsDeleted {get; set;}
}
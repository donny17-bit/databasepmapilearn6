using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("trx_history")]
public class TrxHistory
{
    public int Id {get; set;}

    [Column("fuf_id")]
    public int? FufId {get; set;}

    [Column("frf_id")]
    public int? FrfId {get; set;}

    [Column("fdf_id")]
    public int? FdfId {get; set;}
    
    [Column("trx_action_id")]
    public int TrxActionId {get; set;}

    [Column("trx_status_id")]
    public int TrxStatusId {get; set;}

    [Column("trx_number")]
    public string TrxNumber {get; set;} = null!;

    public string? Description {get; set;}

    [Column("created_by")]
    public int CreatedBy {get; set;}

    [Column("created_date")]
    public DateTime CreatedDate {get; set;}
}
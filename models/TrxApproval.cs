using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace databasepmapilearn6.models;

[Table("trx_approval")]
public class TrxApproval
{
    public int Id {get; set;}

    [Column("approval_detail_id")]
    public int ApprovalDetailId {get; set;}

    [Column("trx_status_id")]
    public int TrxStatusId {get; set;}

    [Column("user_id")]
    public int? UserId {get; set;}

    [Column("fuf_id")]
    public int? FufId {get; set;}

    [Column("frf_id")]
    public int? FrfId {get; set;}

    [Column("fdf_id")]
    public int? FdfId {get; set;}

    public bool Approved {get; set;}
    public string? Description {get; set;}

    [Column("created_date")]
    public DateTime CreatedDate {get; set;}

    [Column("approved_date")]
    public DateTime? ApprovedDate {get; set;}
}
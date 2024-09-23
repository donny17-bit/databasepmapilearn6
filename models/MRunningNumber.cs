using System.ComponentModel.DataAnnotations.Schema;

namespace databasepmapilearn6.models;

[Table("m_running_number")]
public class MRunningNumber
{
    public int Id {get; set;}

    [Column("trx_type_id")]
    public int TrxTypeId {get; set;}

    public int Year {get; set;}

    [Column("running_number")]
    public int RunningNumber {get; set;}
}
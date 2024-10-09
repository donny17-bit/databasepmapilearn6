using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMApproval
{
    public class Detail : VM
    {
        public int id { get; set; }
        public int trx_type_id { get; set; }
        public string trx_type_name { get; set; }
        public List<ApprovalDetail> approval_details { get; set; }


        // constuctor
        private Detail(string CreatedBy, DateTime CreatedDate, string? UpdatedBy, DateTime? UpdatedDate, bool IsDeleted) : base(CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, IsDeleted)
        { }

        public static Detail FromDb(MApproval approval)
        {
            return new Detail(
                approval.CreatedBy.ToString(),
                approval.CreatedDate,
                approval.UpdatedBy.ToString(),
                approval.UpdatedDate,
                approval.IsDeleted
            )
            {
                id = approval.Id,
                trx_type_id = approval.TrxTypeId,
                trx_type_name = approval.TrxType.Name,
                approval_details = approval.MApprovalDetails
                    .Where(m => (m.ApprovalId == approval.Id) && (!m.IsDeleted))
                    .Select(m => new ApprovalDetail
                    {
                        id = m.Id,
                        approval_id = m.ApprovalId,
                        level = m.Level,
                        position_id = m.Position.Id,
                        position_name = m.Position.Name,
                        description = m.Description
                    }).ToList()
            };
        }
    }

    public class ApprovalDetail
    {
        public int id { get; set; }
        public int approval_id { get; set; }
        public int level { get; set; }
        public int position_id { get; set; }
        public string position_name { get; set; }
        public string description { get; set; }
    }
}
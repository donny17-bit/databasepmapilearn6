using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMTrxFuf
{
    public class Table
    {
        public int id { get; set; }
        public string fuf_number { get; set; } = null!;
        public int status_id { get; set; }
        public string status_name { get; set; } = null!;
        public int unit_id { get; set; }
        public string unit_name { get; set; } = null!;
        public int year { get; set; }
        public int project_id { get; set; }
        public string project_name { get; set; } = null!;
        public int job_type_id { get; set; }
        public string job_type_name { get; set; } = null!;

        // nnti aja saat buat detail
        // public VMTrxApproval.Detail[] fuf_approval_list { get; set; } 

        public static Table[] FromDb(TrxFuf[] trxFufs)
        {
            return trxFufs.Select(m => new Table
            {
                id = m.Id,
                fuf_number = m.FufNumber,
                status_id = m.TrxStatusId,
                status_name = m.TrxStatus.Name,
                unit_id = m.UnitId,
                unit_name = m.Unit.Name,
                year = m.Year,
                project_id = m.ProjectId,
                project_name = m.Project.Name,
                job_type_id = m.JobTypeId,
                job_type_name = m.JobType.Name
            }).ToArray();
        }
    }

}
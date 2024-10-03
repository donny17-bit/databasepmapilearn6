namespace databasepmapilearn6.ViewModels;

public abstract class VM
{
    public string created_by { get; set; } = null!;
    public DateTime created_date { get; set; }
    public string? updated_by { get; set; }
    public DateTime? updated_date { get; set; }
    public bool is_deleted { get; set; }

    protected VM(
        string CreatedBy,
        DateTime CreatedDate,
        string? UpdatedBy,
        DateTime? UpdatedDate,
        bool IsDeleted
    )
    {
        this.created_by = CreatedBy;
        this.created_date = CreatedDate;
        this.updated_by = UpdatedBy;
        this.updated_date = UpdatedDate;
        this.is_deleted = IsDeleted;
    }
}
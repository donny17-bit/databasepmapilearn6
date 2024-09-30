namespace databasepmapilearn6.ViewModels;

public abstract class VM
{
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDeleted { get; set; }

    protected VM(
        string CreatedBy,
        DateTime CreatedDate,
        string? UpdatedBy,
        DateTime? UpdatedDate,
        bool IsDeleted
    )
    {
        this.CreatedBy = CreatedBy;
        this.CreatedDate = CreatedDate;
        this.UpdatedBy = UpdatedBy;
        this.UpdatedDate = UpdatedDate;
        this.IsDeleted = IsDeleted;
    }
}
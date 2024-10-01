using Microsoft.EntityFrameworkCore;

namespace databasepmapilearn6.models;

public class DatabasePmContext : DbContext
{
    public DatabasePmContext(DbContextOptions<DatabasePmContext> options) : base(options) { }

    public DbSet<MMenu> MMenus { get; set; } = null!;
    public DbSet<MPosition> MPositions { get; set; } = null!;
    public DbSet<MProject> MProjects { get; set; } = null!;
    public DbSet<MApproval> MApprovals { get; set; } = null!;
    public DbSet<MApprovalDetail> MApprovalDetails { get; set; } = null!;
    public DbSet<MFileCategory>? MFileCategory { get; set; }
    public DbSet<MFileType>? MFileType { get; set; }
    public DbSet<MHoliday>? MHoliday { get; set; }
    public DbSet<MIcon>? MIcon { get; set; }
    public DbSet<MJobFile>? MJobFile { get; set; }
    public DbSet<MJobType>? MJobType { get; set; }
    public DbSet<MRole>? MRole { get; set; }
    public DbSet<MRoleMenu>? MRoleMenu { get; set; }
    public DbSet<MRunningNumber>? MRunningNumber { get; set; }
    public DbSet<MTrx>? MTrx { get; set; }
    public DbSet<MTrxAction>? MTrxAction { get; set; }
    public DbSet<MTrxStatus>? MTrxStatus { get; set; }
    public DbSet<MTrxType>? MTrxType { get; set; }
    public DbSet<MUnit>? MUnit { get; set; }
    public DbSet<MUser>? MUser { get; set; }
    public DbSet<TrxApproval>? TrxApproval { get; set; }
    public DbSet<TrxFdf>? TrxFdf { get; set; }
    public DbSet<TrxFrf>? TrxFrf { get; set; }
    public DbSet<TrxFrfFile>? TrxFrfFile { get; set; }
    public DbSet<TrxFuf>? TrxFuf { get; set; }
    public DbSet<TrxFufFile>? TrxFufFile { get; set; }
    public DbSet<TrxHistory>? TrxHistory { get; set; }
}
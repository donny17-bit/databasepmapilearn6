using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;

namespace databasepmapilearn6.models;

public class DatabasePmContext : DbContext {
    public DatabasePmContext(DbContextOptions<DatabasePmContext> options) : base (options) {}

    public DbSet<MMenu> MMenus {get; set;} = null!;
    public DbSet<MPosition> MPositions {get; set;} = null!;
    public DbSet<MProject> MProjects {get; set;} = null!;
    public DbSet<MApproval> MApprovals {get; set;} = null!;
    public DbSet<MApprovalDetail> MApprovalDetails {get; set;} = null!;
    public DbSet<MFileCategory>? MFileCategory { get; set; }
    public DbSet<MFileType>? MFileType { get; set; }
    public DbSet<databasepmapilearn6.models.MHoliday>? MHoliday { get; set; }
    public DbSet<databasepmapilearn6.models.MIcon>? MIcon { get; set; }
    public DbSet<databasepmapilearn6.models.MJobFile>? MJobFile { get; set; }
    public DbSet<databasepmapilearn6.models.MJobType>? MJobType { get; set; }
    public DbSet<databasepmapilearn6.models.MRole>? MRole { get; set; }
    public DbSet<databasepmapilearn6.models.MRoleMenu>? MRoleMenu { get; set; }
    public DbSet<databasepmapilearn6.models.MRunningNumber>? MRunningNumber { get; set; }
    public DbSet<databasepmapilearn6.models.MTrx>? MTrx { get; set; }
    public DbSet<databasepmapilearn6.models.MTrxAction>? MTrxAction { get; set; }
    public DbSet<databasepmapilearn6.models.MTrxStatus>? MTrxStatus { get; set; }
    public DbSet<databasepmapilearn6.models.MTrxType>? MTrxType { get; set; }
    public DbSet<databasepmapilearn6.models.MUnit>? MUnit { get; set; }
    public DbSet<databasepmapilearn6.models.MUser>? MUser { get; set; }
    public DbSet<databasepmapilearn6.models.TrxApproval>? TrxApproval { get; set; }
}
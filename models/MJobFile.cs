using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace databasepmapilearn6.models;

[Table("m_job_file")]
public class MJobFile
{
    public int Id {get; set;}

    [Column("user_mgr")]
    public int UserMgr {get; set;}

    [Column("fuf_id")]
    public int FufId {get; set;}

    [Column("pic_name")]
    public string PicName {get; set;} = null!;
    public int Year {get; set;}
    
    [Column("unit_id")]
    public int UnitId {get; set;}
    
    [Column("project_id")]
    public int ProjectId {get; set;}

    [Column("job_type_id")]
    public int JobTypeId {get; set;}

    [Column("file_type_id")]
    public int FileTypeId {get; set;}

    [Column("file_category_id")]
    public int FileCategoryId {get; set;}

    [Column("file_name")]
    public string FileName {get; set;} = null!;

    [Column("file_path")]
    public string FilePath {get; set;} = null!;

    public string Description {get; set;} = null!;

    [Column("created_by")]
    public int CreatedBy {get; set;}

    [Column("created_date")]
    public DateTime CreatedDate {get; set;}

    [Column("updated_by")]
    public int? UpdatedBy {get; set;}

    [Column("updated_date")]
    public DateTime? UpdatedDate {get; set;}

    [Column("is_deleted")]
    public bool IsDeleted {get; set;}
}
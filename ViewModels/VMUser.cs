using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMUser
{
    public class Detail : VM
    {
        public int Id {get; set;}
        public int RoleId {get; set;}
        public int PositionId {get; set;}
        public string Username {get; set;} = null!;
        public string Name {get; set;} = null!;
        public string Email {get; set;} = null!;

        private Detail(string CreatedBy, DateTime CreatedDate, string? UpdateBy, DateTime? UpdateDate, bool IsDeleted) : base(CreatedBy, CreatedDate, UpdateBy, UpdateDate, IsDeleted)
        {
        }

        public static Detail FromDb(MUser user) 
        {
            return new Detail(user.CreatedBy.ToString(), user.CreatedDate, user.UpdatedBy.ToString(), user.UpdatedDate, user.IsDeleted)
            {
                Id = user.Id,
                RoleId = user.RoleId,
                PositionId = user.PositionId,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
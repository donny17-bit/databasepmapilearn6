using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMUser
{
    public class Detail : VM
    {
        public int id { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }
        public int position_id { get; set; }
        public string position_name { get; set; }
        public string username { get; set; } = null!;
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;

        private Detail(string CreatedBy, DateTime CreatedDate, string? UpdateBy, DateTime? UpdateDate, bool IsDeleted) : base(CreatedBy, CreatedDate, UpdateBy, UpdateDate, IsDeleted)
        {
        }

        public static Detail FromDb(MUser user)
        {
            return new Detail(user.CreatedBy.ToString(), user.CreatedDate, user.UpdatedBy.ToString(), user.UpdatedDate, user.IsDeleted)
            {
                id = user.Id,
                role_id = user.RoleId,
                role_name = user.Role.Name,
                position_id = user.PositionId,
                position_name = user.Position.Name,
                username = user.Username,
                name = user.Name,
                email = user.Email
            };
        }
    }
}
using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMUser
{
    public class Dropdown : VMDropdown
    {
        public Dropdown(string value, string text) : base(value, text) { }

        public static Dropdown[] FromDb(MUser[] users)
        {
            return users.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        }
    }

    public class Table
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string position_code { get; set; }
        public string position_name { get; set; }
        public int role_id { get; set; }
        public string role_name { get; set; }

        // constructor
        private Table() { }

        public static Table[] FromDb(MUser[] users)
        {
            return users.Select(m => new Table
            {
                id = m.Id,
                username = m.Username,
                name = m.Name,
                email = m.Email,
                position_code = m.Position.Code,
                role_id = m.RoleId,
                role_name = m.Role.Name,
            }).ToArray();
        }
    }

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
            return new Detail(
                user.CreatedBy.ToString(),
                user.CreatedDate,
                user.UpdatedBy.ToString(),
                user.UpdatedDate,
                user.IsDeleted)
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
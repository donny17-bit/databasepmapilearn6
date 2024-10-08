using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMRole
{
    public class Detail : VM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<MenuList> menu_list { get; set; } = null!;

        private Detail(string CreatedBy, DateTime CreatedDate, string? UpdateBy, DateTime? UpdateDate, bool IsDeleted) : base(CreatedBy, CreatedDate, UpdateBy, UpdateDate, IsDeleted)
        {
        }

        public static Detail FromDb(MRole mRole)
        {
            return new Detail(mRole.CreatedBy.ToString(), mRole.CreatedDate, mRole.UpdatedBy.ToString(), mRole.UpdatedDate, mRole.IsDeleted)
            {
                Id = mRole.Id,
                Name = mRole.Name,
                menu_list = mRole.RoleMenus.Select(m => new MenuList
                {
                    menu_id = m.Menu.ID,
                    icon_name = m.Menu.Icon.Name,
                    menu_name = m.Menu.Name
                }).ToList()
            };
        }
    }

    public class MenuList
    {
        public int menu_id { get; set; }
        public string icon_name { get; set; } = null!;
        public string menu_name { get; set; } = null!;
    }

    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MRole[] role)
        {
            return role.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
        }
    }

    public class Table
    {
        public int id { get; set; }
        public string name { get; set; }

        public static Table[] FromDb(MRole[] role)
        {
            return role.Select(m => new Table
            {
                id = m.Id,
                name = m.Name,
            }).ToArray();
        }
    }
}
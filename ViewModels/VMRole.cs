using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMRole
{
    public class Detail : VM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<MenuList> MenuLists { get; set; } = null!;

        private Detail(string CreatedBy, DateTime CreatedDate, string? UpdateBy, DateTime? UpdateDate, bool IsDeleted) : base(CreatedBy, CreatedDate, UpdateBy, UpdateDate, IsDeleted)
        {
        }

        public static Detail FromDb(MRole mRole)
        {
            return new Detail(mRole.CreatedBy.ToString(), mRole.CreatedDate, mRole.UpdatedBy.ToString(), mRole.UpdatedDate, mRole.IsDeleted)
            {
                Id = mRole.Id,
                Name = mRole.Name,
                MenuLists = mRole.RoleMenus.Select(m => new MenuList
                {
                    MenuId = m.Menu.ID,
                    IconName = m.Menu.Icon.Name,
                    MenuName = m.Menu.Name
                }).ToList()
            };
        }
    }

    public class MenuList
    {
        public int MenuId { get; set; }
        public string IconName { get; set; } = null!;
        public string MenuName { get; set; } = null!;
    }

    public class Dropdown : VMDropdown
    {
        private Dropdown(string value, string text) : base(value, text)
        { }

        public static Dropdown[] FromDb(MRole[] role) => role.Select(m => new Dropdown(m.Id.ToString(), m.Name)).ToArray();
    }
}
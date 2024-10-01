using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMRoleMenu
{
    public class Menu
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public int IconId { get; set; }
        // public string IconName { get; set; } = null!;
        public int? ParentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Component { get; set; }
        public string? Path { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; }

        // this is use to change the return to be like the properties above
        public static List<Menu> FromDb(List<MRoleMenu> mRoleMenus)
        {
            return mRoleMenus.Select(mRoleMenu => new Menu
            {
                Id = mRoleMenu.Id,
                RoleId = mRoleMenu.RoleId,
                MenuId = mRoleMenu.MenuId,
                IconId = mRoleMenu.Menu.IconId,
                // IconName = mRoleMenu.
                ParentId = mRoleMenu.Menu.ParentId,
                Name = mRoleMenu.Menu.Name,
                Component = mRoleMenu.Menu.Component,
                Path = mRoleMenu.Menu.Path,
                Order = mRoleMenu.Menu.Order,
                IsDeleted = mRoleMenu.Menu.IsDeleted
            }).ToList();
        }
    }
}
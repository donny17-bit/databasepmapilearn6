using databasepmapilearn6.models;

namespace databasepmapilearn6.ViewModels;

public class VMRoleMenu
{
    public class Menu
    {
        public int id { get; set; }
        public int role_id { get; set; }
        public int menu_id { get; set; }
        public int icon_id { get; set; }
        public string icon_name { get; set; } = null!;
        public int? parent_id { get; set; }
        public string name { get; set; } = null!;
        public string? component { get; set; }
        public string? path { get; set; }
        public int order { get; set; }

        public bool is_deleted { get; set; }

        // this is use to change the return to be like the properties above
        public static List<Menu> FromDb(List<MRoleMenu> mRoleMenus)
        {
            return mRoleMenus.Select(mRoleMenu => new Menu
            {
                id = mRoleMenu.Id,
                role_id = mRoleMenu.RoleId,
                menu_id = mRoleMenu.MenuId,
                icon_id = mRoleMenu.Menu.IconId,
                icon_name = mRoleMenu.Menu.Icon.Name,
                parent_id = mRoleMenu.Menu?.ParentId,
                name = mRoleMenu.Menu.Name,
                component = mRoleMenu.Menu?.Component,
                path = mRoleMenu.Menu?.Path,
                order = mRoleMenu.Menu.Order,
                is_deleted = mRoleMenu.Menu.IsDeleted
            }).ToList();
        }
    }
}
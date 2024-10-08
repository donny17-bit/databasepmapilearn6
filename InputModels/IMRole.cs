using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

public class IMRole
{

    public class Dropdown : IMDropdown
    {
    }

    public class Table : IMTable
    {
    }

    public class CreateRole
    {
        [Required]
        public string name { get; set; } = null!;

        [Required]
        public List<int> menu_id { get; set; } = null!;
        // public int MenuId { get; set; }
    }
    public class EditRole
    {
        [Required]
        public string name { get; set; } = null!;

        [Required(ErrorMessage = "Please choose minimal 1 menu")]
        public List<int> menu_id { get; set; } = null!;
    }
}
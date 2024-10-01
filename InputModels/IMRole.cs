using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

public class IMRole
{
    public class CreateRole
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public List<int> MenuId { get; set; } = null!;
        // public int MenuId { get; set; }
    }
    public class EditRole
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please choose minimal 1 menu")]
        public List<int> MenuId { get; set; } = null!;
    }
}
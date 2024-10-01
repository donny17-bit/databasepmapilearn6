using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

public class IMRole
{
    public class Edit
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please choose minimal 1 menu")]
        public List<int> MenuId { get; set; } = null!;
    }
}
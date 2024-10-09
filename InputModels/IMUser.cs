using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;

public class IMUser
{
    public class Create
    {
        [Required]
        public int role_id { get; set; }

        [Required]
        public int position_id { get; set; }

        [Required(ErrorMessage = "username tidak boleh kosong")]
        public string Username { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

    }

    public class Edit
    {
        [Required]
        public int role_id { get; set; }

        [Required]
        public int position_id { get; set; }

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
    }

    public class Table : IMTable
    {
    }

    public class Dropdown : IMDropdown
    {
        public int[] position_ids { get; set; }
    }
}
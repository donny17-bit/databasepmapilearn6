using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.InputModels;


public class IMAuth
{
    public class Login
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class ChangePassword
    {
        [Required]
        public string old_pass { get; set; } = null!;

        [Required]
        public string new_pass { get; set; } = null!;
    }
}
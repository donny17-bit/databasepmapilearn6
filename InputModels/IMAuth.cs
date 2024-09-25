using System.ComponentModel.DataAnnotations;

namespace databasepmapilearn6.models;


public class IMAuth
{
    public class Login
    {   
        [Required]
        public string Username {get; set;} = null!;

        [Required]
        public string Password {get; set;} = null!;
    }
}
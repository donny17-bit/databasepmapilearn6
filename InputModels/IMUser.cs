using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CodeActions;

namespace databasepmapilearn6.models;

public class IMUser
{
    public class Create
    {
        [Required]
        public int RoleId {get; set;}

        [Required]
        public int PositionId {get; set;}

        [Required]
        public string Username {get; set;} = null!;

        [Required]
        public string Name {get; set;} = null!;

        [EmailAddress]
        [Required]
        public string Email {get; set;} = null!;


        // buat sementara user input password saat create akun 
        // nnti dihapus (auto create random password)
        [Required]
        public string Password {get; set;} = null!;
    }

    public class Input
    {

    }
}
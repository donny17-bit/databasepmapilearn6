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

    }

    public class Edit
    {
        [Required]
        public int RoleId {get; set;}

        [Required]
        public int PositionId {get; set;}

        [Required]
        public string Username {get; set;} = null!;

        [Required]
        public string Name {get; set;} = null!;

        [Required]
        public string Email {get; set;} = null!;
    }
}
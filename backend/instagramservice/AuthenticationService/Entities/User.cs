using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Entities
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required]
        public string Password { get; set; }
        
        [Column(TypeName = "varchar(30)")]
        [Required]
        public string FirstName { get; set; }
        
        [Column(TypeName = "varchar(30)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(20)")]
        [Required]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        public string CountryCode { get; set; }

        [Column(TypeName = "bit")]
        [Required]
        public bool IsActive { get; set; }

        [Column(TypeName = "datetime")]
        [Required]
        public DateTime CreatedTs { get; private set; } = DateTime.UtcNow;

    }
}

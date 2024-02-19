using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AgileBoardAPI.Models
{
    [Table("user")]
    [Index(nameof(Email), IsUnique = true, Name = "email_constraint")]
    [Index(nameof(Username), IsUnique = true, Name = "username_constraint")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id", TypeName = "BINARY(16)")]
        public Guid Id { get; set; }

        [Required]
        [Column("first_name", TypeName = "VARCHAR(255)")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name", TypeName = "VARCHAR(255)")]
        public string LastName { get; set; }

        [Required]
        [Column("username", TypeName = "VARCHAR(255)")]
        public string Username { get; set; }

        [Required]
        [Column("email", TypeName = "VARCHAR(255)")]
        public string Email { get; set; }

        [Required]
        [Column("password", TypeName = "VARCHAR(255)")]
        public string Password { get; set; }

        [Column("enabled")]
        public bool? IsEnabled { get; set; }
    }
}

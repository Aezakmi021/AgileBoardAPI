using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgileBoardAPI.Models
{
    [Table("project")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("project_id", TypeName = "BINARY(16)")]
        public Guid Id { get; set; }

        [Required]
        [Column("name", TypeName = "VARCHAR(255)")]
        public string Name { get; set; }

        [Required]
        [Column("description", TypeName = "TEXT")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("User")]
        [Column("owner_username", TypeName = "VARCHAR(255)")]
        public string OwnerUsername { get; set; }

        public virtual User User { get; set; }
    }
}

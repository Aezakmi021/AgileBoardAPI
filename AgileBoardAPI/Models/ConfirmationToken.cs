using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgileBoardAPI.Models
{
    [Table("confirmation_token")]
    public class ConfirmationToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("token_id", TypeName = "BINARY(16)")]
        public Guid Id { get; set; }

        [Required]
        [Column("token")]
        public string Token { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}

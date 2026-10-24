using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homestay.api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        [Column("Password")]
        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
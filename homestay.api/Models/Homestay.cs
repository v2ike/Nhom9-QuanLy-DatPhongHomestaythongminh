using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homestay.api.Models
{
    [Table("Homestays")]
    public class Homestay
    {
        [Key]
        [Column("Idhome")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int HostId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
        [NotMapped]
        public List<string> Images { get; set; } = new();
        public List<Tiennghis> TienNghi { get; set; } = new();
        public List<Room> Rooms { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}

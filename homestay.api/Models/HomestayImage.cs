using System.ComponentModel.DataAnnotations.Schema;

namespace homestay.api.Models
{
    [Table("HomestayImages")]
    public class HomestayImage
    {
        
        [Column("Idimage")]
        public int Id { get; set; }
        [Column("Idhome")]
        public int HomestayId { get; set; }
        [Column("ImageUrl")]
        public string ImageUrl { get; set; }
    }
}

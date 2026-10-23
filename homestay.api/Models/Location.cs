using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace homestay.api.Models
{
    [Table("locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}

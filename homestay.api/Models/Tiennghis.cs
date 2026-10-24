using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homestay.api.Models
{
    [Table("Tiennghi")]  
    public class Tiennghis
    {
        [Key]
        [Column("Idtiennghi")]
        public int Idtiennghi { get; set; }
        [Column("Idhome")]// Hoặc Idtiennghi
        public int HomestayId { get; set; }
        public string Icon { get; set; }
        [Column("Tittle")]
        public string Tittle { get; set; }
    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     }                   

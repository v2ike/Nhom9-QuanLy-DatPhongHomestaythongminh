using System;
using System.Collections.Generic;
using System.Text;

namespace Quanlyvadatphonghomestay.Models
{
    public class Homestay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int HostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public double Rating { get; set; } = 4.8;
        public int ReviewCount { get; set; } = 256;

        public ObservableCollection<Room> Rooms { get; set; } = new();
        public ObservableCollection<string> Images { get; set; } = new();
        public ObservableCollection<TienNghi> Amenities { get; set; } = new();
        public ObservableCollection<Review> Reviews { get; set; } = new();
        public string ImageCountText => $"{Images?.Count ?? 1}/12";
        public string ReviewCountText => $"({ReviewCount} đánh giá)";
    }
}

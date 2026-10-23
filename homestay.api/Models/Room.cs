namespace homestay.api.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int HomestayId { get; set; }

        public string RoomName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }
    }
}

namespace homestay.api.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int Guests { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

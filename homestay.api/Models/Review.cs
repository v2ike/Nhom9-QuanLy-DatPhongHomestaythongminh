namespace homestay.api.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int HomestayId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

namespace homestay.api.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int HomestayId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

namespace homestay.api.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public string TransactionCode { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}

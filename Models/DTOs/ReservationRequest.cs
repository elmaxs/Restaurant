namespace Restaurant.Models.DTOs
{
    public class ReservationRequest
    {
        public Guid TableId { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationHours { get; set; }
    }
}

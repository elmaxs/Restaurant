namespace Restaurant.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }

        public DateTime TimeReservation { get; set; }
    }
}

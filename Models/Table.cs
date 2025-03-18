namespace Restaurant.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; } // Кількість місць
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

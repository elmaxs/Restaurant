using Restaurant.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Table
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public int Capacity { get; set; } // Кількість місць
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerHour { get; set; }

        public TableStatus Status { get; set; } = TableStatus.Default;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}

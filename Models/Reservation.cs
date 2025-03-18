using Restaurant.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }

        public Guid TableId { get; set; }
        public Table Table { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddHours(DurationHours); // Автоматический расчет
        public int DurationHours { get; set; } // Длительность брони в часах
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrepaymentAmount { get; set; } // 1000 грн за час
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания
        public string? Comment { get; set; } // Дополнительный комментарий пользователя
    }
}

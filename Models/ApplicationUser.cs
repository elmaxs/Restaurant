using Microsoft.AspNetCore.Identity;

namespace Restaurant.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public string Name { get; set; }
    }
}

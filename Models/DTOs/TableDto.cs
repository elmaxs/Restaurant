using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs
{
    public class TableDto
    {
        [Required, Range(1, 100)]
        public int TableNumber { get; set; }

        [Required, Range(1, 20)]
        public int Capacity { get; set; }
    }
}

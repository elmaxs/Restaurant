using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs
{
    public class MenuItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required, Range(1, 10000)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}

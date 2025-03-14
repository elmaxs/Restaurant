using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }

        public MenuItem MenuItem { get; set; }
        public Guid MenuItemId { get; set; }

        public string Name { get; set; }
        public int Gram { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Calories { get; set; }
    }
}

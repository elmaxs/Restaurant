namespace Restaurant.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }

        public MenuItem MenuItem { get; set; }
        public Guid MenuItemId { get; set; }

        public string Name { get; set; }
        public int Gram { get; set; }
        public decimal Calories { get; set; }
    }
}

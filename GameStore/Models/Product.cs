using System.ComponentModel;

namespace GameStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public Category Category { get; set; }
    }

    public enum Category
    {
        Racing,
        RPG,
        FPS
    }
}

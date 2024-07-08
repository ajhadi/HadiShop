namespace HadiShop.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

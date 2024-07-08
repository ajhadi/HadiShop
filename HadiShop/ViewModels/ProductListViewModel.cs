using HadiShop.Models.Entities;

namespace HadiShop.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string SearchQuery { get; set; }
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}

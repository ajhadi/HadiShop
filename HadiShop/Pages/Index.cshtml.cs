// Pages/Index.cshtml.cs
using HadiShop.Models.Entities;
using HadiShop.Services;
using HadiShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HadiShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public ProductListViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchQuery = "", int selectedCategoryId = 0)
        {
            var products = await _productService.GetProductsAsync(searchQuery, selectedCategoryId);
            var categories = await _productService.GetCategoriesAsync();

            ViewModel = new ProductListViewModel
            {
                Products = products,
                SearchQuery = searchQuery,
                SelectedCategoryId = selectedCategoryId,
                Categories = categories
            };

            return Page();
        }
    }
}

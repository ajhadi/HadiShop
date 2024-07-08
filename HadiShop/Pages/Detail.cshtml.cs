using HadiShop.Services;
using HadiShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HadiShop.Pages
{
    public class DetailModel : PageModel
    {
        private readonly ProductService _productService;

        public DetailModel(ProductService productService)
        {
            _productService = productService;
        }

        public ProductDetailViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewModel = new ProductDetailViewModel
            {
                Product = product
            };

            return Page();
        }
    }
}

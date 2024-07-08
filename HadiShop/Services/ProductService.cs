using HadiShop.Data;
using HadiShop.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HadiShop.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public ProductService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchQuery, int selectedCategoryId)
        {
            var productsQuery = _context.Products.Include(p => p.ProductCategories).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (selectedCategoryId > 0)
            {
                productsQuery = productsQuery.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == selectedCategoryId));
            }

            return await productsQuery.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            const string cacheKey = "all_categories";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Category> categories))
            {
                categories = await _context.Categories.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                _cache.Set(cacheKey, categories, cacheEntryOptions);
            }

            return categories;
        }
    }
}

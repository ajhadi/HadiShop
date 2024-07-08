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

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string searchQuery, int selectedCategoryId)
        {
            var cacheKey = "all_products";
            if (!_cache.TryGetValue(cacheKey, out IQueryable<Product>? cachedProducts))
            {
                var products = await _context.Products.Include(p => p.ProductCategories).ToArrayAsync();
                cachedProducts = products.AsQueryable();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    Priority = CacheItemPriority.Normal
                };

                _cache.Set(cacheKey, cachedProducts, cacheEntryOptions);
            }
            if (!string.IsNullOrWhiteSpace(searchQuery) && cachedProducts != null)
            {
                cachedProducts = cachedProducts.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (selectedCategoryId > 0 && cachedProducts != null)
            {
                cachedProducts = cachedProducts.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == selectedCategoryId));
            }

            return cachedProducts ?? Enumerable.Empty<Product>();
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

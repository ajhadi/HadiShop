using HadiShop.Data;
using HadiShop.Models.Entities;
using HadiShop.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class DatabaseInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<DatabaseInitializer> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task Initialize()
    {
        try
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (await _userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Gender = Gender.Male,
                    PhoneNumberConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    _logger.LogError($"Failed to create admin user. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            if (!await _context.Categories.AnyAsync())
            {
                _context.Categories.AddRange(
                    new Category { Name = "Electronics", Description = "Electronic Items" },
                    new Category { Name = "Books", Description = "Books and Literature" },
                    new Category { Name = "Clothing", Description = "Men and Women Clothing" }
                );

                await _context.SaveChangesAsync();
            }

            if (!await _context.Products.AnyAsync())
            {
                _context.Products.AddRange(
                    new Product { Name = "Laptop", Description = "High performance laptop", Price = 999.99m, Stock = 10, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99m, Stock = 15, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Tablet", Description = "10 inch tablet", Price = 499.99m, Stock = 20, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Headphones", Description = "Noise cancelling headphones", Price = 199.99m, Stock = 30, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Monitor", Description = "27 inch 4K monitor", Price = 299.99m, Stock = 25, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Keyboard", Description = "Mechanical keyboard", Price = 99.99m, Stock = 50, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Fiction Book", Description = "Popular fiction book", Price = 14.99m, Stock = 100, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Non-Fiction Book", Description = "Informative non-fiction book", Price = 19.99m, Stock = 80, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Men's T-Shirt", Description = "Comfortable men's t-shirt", Price = 24.99m, Stock = 60, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Women's Dress", Description = "Elegant women's dress", Price = 49.99m, Stock = 40, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Jacket", Description = "Warm winter jacket", Price = 89.99m, Stock = 35, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow },
                    new Product { Name = "Sneakers", Description = "Comfortable sneakers", Price = 59.99m, Stock = 45, ImageUrl = "/img/product.png", CreatedAt = DateTime.UtcNow }
                );

                await _context.SaveChangesAsync();
            }

            if (!await _context.ProductCategories.AnyAsync())
            {
                var categories = await _context.Categories.ToListAsync();
                var products = await _context.Products.ToListAsync();

                var electronicsCategory = categories.FirstOrDefault(c => c.Name == "Electronics");
                var booksCategory = categories.FirstOrDefault(c => c.Name == "Books");
                var clothingCategory = categories.FirstOrDefault(c => c.Name == "Clothing");

                var laptop = products.FirstOrDefault(p => p.Name == "Laptop");
                var smartphone = products.FirstOrDefault(p => p.Name == "Smartphone");
                var tablet = products.FirstOrDefault(p => p.Name == "Tablet");
                var headphones = products.FirstOrDefault(p => p.Name == "Headphones");
                var monitor = products.FirstOrDefault(p => p.Name == "Monitor");
                var keyboard = products.FirstOrDefault(p => p.Name == "Keyboard");
                var fictionBook = products.FirstOrDefault(p => p.Name == "Fiction Book");
                var nonFictionBook = products.FirstOrDefault(p => p.Name == "Non-Fiction Book");
                var mensTshirt = products.FirstOrDefault(p => p.Name == "Men's T-Shirt");
                var womensDress = products.FirstOrDefault(p => p.Name == "Women's Dress");
                var jacket = products.FirstOrDefault(p => p.Name == "Jacket");
                var sneakers = products.FirstOrDefault(p => p.Name == "Sneakers");

                var categoryMappings = new List<ProductCategory>();

                if (electronicsCategory != null)
                {
                    if (laptop != null) categoryMappings.Add(new ProductCategory { ProductId = laptop.ProductId, CategoryId = electronicsCategory.CategoryId });
                    if (smartphone != null) categoryMappings.Add(new ProductCategory { ProductId = smartphone.ProductId, CategoryId = electronicsCategory.CategoryId });
                    if (tablet != null) categoryMappings.Add(new ProductCategory { ProductId = tablet.ProductId, CategoryId = electronicsCategory.CategoryId });
                    if (headphones != null) categoryMappings.Add(new ProductCategory { ProductId = headphones.ProductId, CategoryId = electronicsCategory.CategoryId });
                    if (monitor != null) categoryMappings.Add(new ProductCategory { ProductId = monitor.ProductId, CategoryId = electronicsCategory.CategoryId });
                    if (keyboard != null) categoryMappings.Add(new ProductCategory { ProductId = keyboard.ProductId, CategoryId = electronicsCategory.CategoryId });
                }

                if (booksCategory != null)
                {
                    if (fictionBook != null) categoryMappings.Add(new ProductCategory { ProductId = fictionBook.ProductId, CategoryId = booksCategory.CategoryId });
                    if (nonFictionBook != null) categoryMappings.Add(new ProductCategory { ProductId = nonFictionBook.ProductId, CategoryId = booksCategory.CategoryId });
                }

                if (clothingCategory != null)
                {
                    if (mensTshirt != null) categoryMappings.Add(new ProductCategory { ProductId = mensTshirt.ProductId, CategoryId = clothingCategory.CategoryId });
                    if (womensDress != null) categoryMappings.Add(new ProductCategory { ProductId = womensDress.ProductId, CategoryId = clothingCategory.CategoryId });
                    if (jacket != null) categoryMappings.Add(new ProductCategory { ProductId = jacket.ProductId, CategoryId = clothingCategory.CategoryId });
                    if (sneakers != null) categoryMappings.Add(new ProductCategory { ProductId = sneakers.ProductId, CategoryId = clothingCategory.CategoryId });
                }

                if (categoryMappings.Any())
                {
                    _context.ProductCategories.AddRange(categoryMappings);
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

}

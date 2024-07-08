using HadiShop.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace HadiShop.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = "User";
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateOnly? Birthday { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

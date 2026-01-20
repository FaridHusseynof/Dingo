using Dingo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dingo.Data
{
    public class DingoDbContext : IdentityDbContext<AppUser>
    {
        public DingoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
    }
}

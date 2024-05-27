using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewKhumaloCraft.Models;

namespace NewKhumaloCraft.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SalesEntity> Sales { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<NewKhumaloCraft.Models.PurchaseDetails> PurchaseDetails { get; set; } = default!;
    }
}

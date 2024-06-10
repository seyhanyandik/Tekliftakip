using Tekliftakip.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tekliftakip.Models;

namespace Tekliftakip.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Stock>? Stocks { get; set; }

        public DbSet<Product>? Products { get; set; }

        public DbSet<Customer>? Customers { get; set; }

        public DbSet<CurrentAccount>? CurrentAccounts { get; set; }

        public DbSet<BidForm>? BidForms { get; set; }

        public DbSet<BidCart>? BidCarts { get; set; }

        public DbSet<Slider>? Sliders { get; set; }
        





    }

}

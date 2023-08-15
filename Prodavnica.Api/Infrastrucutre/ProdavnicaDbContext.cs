using Microsoft.EntityFrameworkCore;
using Prodavnica.Api.Models;

namespace Prodavnica.Api.Infrastructure
{
    public class ProdavnicaDbContext : DbContext
    {
        public ProdavnicaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdavnicaDbContext).Assembly);
            modelBuilder.Entity<Oreder>().HasKey(o => o.Id);;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<Oreder> Orders { get; set; }
    }
}

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
        }

        public DbSet<User> Users { get; set; }
    }
}

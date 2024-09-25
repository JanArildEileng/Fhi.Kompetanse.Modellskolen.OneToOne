using Microsoft.EntityFrameworkCore;
using Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Entities;

namespace Fhi.Kompetanse.Modellskolen.OneToOne.WebApi.Data.Context
{
    public class KompetanseContext : DbContext
    {
        public KompetanseContext(DbContextOptions<KompetanseContext> options)
            : base(options)
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, new[]{DbLoggerCategory.Database.Command.Name},LogLevel.Information);
        }

        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<King> Kings { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasOne<King>(e=>e.King)
                .WithOne(e => e.Country)
                .HasForeignKey<King>(e=>e.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

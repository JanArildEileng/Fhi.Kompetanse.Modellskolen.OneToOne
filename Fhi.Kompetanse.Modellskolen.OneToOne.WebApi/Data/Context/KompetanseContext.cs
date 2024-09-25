
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
       //     optionsBuilder.LogTo(Console.WriteLine, new[]{DbLoggerCategory.Database.Command.Name},LogLevel.Information);
        }

        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<King> Kings { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //TODO Add her
            /*
               modelBuilder.Entity<Country>()...
             
             */ 
         
        }

    }
}

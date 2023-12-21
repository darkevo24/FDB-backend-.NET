using FDB_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace FDB_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<YourDetails> YourDetails { get; set; }
        public DbSet<TheirDetails> TheirDetails { get; set; }
        public DbSet<DebtOwed> DebtOwed { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure YourDetails entity
            modelBuilder.Entity<YourDetails>()
                .HasKey(yd => yd.Id); // Assuming YourDetailsId is the primary key property

            // Configure TheirDetails entity
            modelBuilder.Entity<TheirDetails>()
                .HasKey(td => td.Id); // Assuming TheirDetailsId is the primary key property

            // Configure DebtOwed entity
            modelBuilder.Entity<DebtOwed>()
                .HasKey(td => td.Id); // Assuming DebtOwedId is the primary key property
            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

        // Other configurations and methods as needed

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure async behavior if needed
            optionsBuilder.UseSqlServer("DefaultConnection");
        }
    }
}

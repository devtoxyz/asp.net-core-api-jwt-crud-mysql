using Microsoft.EntityFrameworkCore;

namespace AuthCrud.Models
{
    public class ModelContext: DbContext
    {
        static readonly string connectionString = Environment.GetEnvironmentVariable("DBConnectionStrings")!;

        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

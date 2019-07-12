using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public sealed class StoreLocatorContext : DbContext
    {
        public StoreLocatorContext(DbContextOptions<StoreLocatorContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreType>()
                .HasAlternateKey(st => st.Name);
            modelBuilder.Entity<StoreType>()
                .Property(st => st.Updated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Store>()
                .Property(s => s.Location)
                .HasComputedColumnSql(
                    "case " +
                    "when [lat] is null or [lng] is null then null " +
                    $"else geography::Point([lat], [lng], {Constants.DefaultSRID}) " +
                    "end persisted");
            modelBuilder.Entity<Store>()
                .Property(s => s.Updated)
                .HasDefaultValueSql("getdate()");
        }
    }
}

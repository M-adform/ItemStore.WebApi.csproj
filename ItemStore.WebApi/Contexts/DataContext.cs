using ItemStore.WebApi.csproj.Models.Entities;
using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.csproj.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }

        public DataContext(DbContextOptions<DataContext>
            options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasOne<Shop>(e => e.Shop)
                .WithMany(d => d.Items)
                .HasForeignKey(e => e.ShopId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
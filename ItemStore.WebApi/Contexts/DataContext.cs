using ItemStore.WebApi.csproj.Models.Entities;
using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.csproj.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DataContext(DbContextOptions<DataContext>
            options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().ToTable("items");
        }
    }
}

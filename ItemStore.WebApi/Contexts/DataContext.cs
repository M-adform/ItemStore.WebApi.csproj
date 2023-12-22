using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.csproj.Contexts
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Item> Items { get; set; }

        public DataContext(DbContextOptions<DataContext>
            options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().ToTable("items");
        }
    }
}

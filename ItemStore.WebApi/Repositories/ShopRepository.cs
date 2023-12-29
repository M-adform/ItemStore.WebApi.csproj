using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.csproj.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly DataContext _dataContext;

        public ShopRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Shop>> GetShopsAsync()
        {
            return await _dataContext.Shops.ToListAsync();
        }

        public async Task<Shop?> GetShopByIdAsync(int? id)
        {
            return await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Shop?> GetShopByNameAsync(string name)
        {
            return await _dataContext.Shops.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<Shop> AddShopAsync(Shop shop)
        {
            _dataContext.Shops.Add(shop);
            await _dataContext.SaveChangesAsync();
            return shop;
        }

        public async Task UpdateShopByIdAsync(int id, Shop shop)
        {
            var shopToUpdate = await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
            if (shopToUpdate == null)
                return;

            shopToUpdate.Name = shop.Name;
            shopToUpdate.Address = shop.Address;
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteShopByIdAsync(int id)
        {
            var shopToDelete = await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
            if (shopToDelete == null)
                return;

            _dataContext.Remove(shopToDelete);
            await _dataContext.SaveChangesAsync();
        }
    }
}
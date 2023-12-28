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

        public async Task<Shop?> GetShopByIdAsync(int id)
        {
            return await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Shop?> GetShopByNameAsync(string name)
        {
            return await _dataContext.Shops.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<int> AddShopAsync(Shop shop)
        {
            _dataContext.Shops.Add(shop);
            await _dataContext.SaveChangesAsync();
            return shop.Id;
        }

        public async Task UpdateShopByIdAsync(int id, Shop shop)
        {
            var itemToUpdate = await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
            if (itemToUpdate == null)
                return;

            itemToUpdate.Name = shop.Name;
            itemToUpdate.Adress = shop.Adress;
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteShopByIdAsync(int id)
        {
            var itemToDelete = await _dataContext.Shops.FirstOrDefaultAsync(i => i.Id == id);
            if (itemToDelete == null)
                return;

            _dataContext.Remove(itemToDelete);
            await _dataContext.SaveChangesAsync();
        }
    }
}

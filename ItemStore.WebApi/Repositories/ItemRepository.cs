using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _dataContext;

        public ItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _dataContext.Items.ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(Guid id)
        {
            return await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Item?> GetItemByNameAsync(string name)
        {
            return await _dataContext.Items.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemByIdAsync(Guid id, Item item)
        {
            var itemToUpdate = await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (itemToUpdate == null)
                return;

            itemToUpdate.Name = item.Name;
            itemToUpdate.Price = item.Price;
            itemToUpdate.ShopId = item.ShopId;
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteItemByIdAsync(Guid id)
        {
            var itemToDelete = await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (itemToDelete == null)
                return;

            _dataContext.Remove(itemToDelete);
            await _dataContext.SaveChangesAsync();
        }
    }
}
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Repositories
{
    public interface IItemRepository
    {
        Task<Item> AddItemAsync(Item item);

        Task DeleteItemByIdAsync(Guid id);

        Task<Item?> GetItemByIdAsync(Guid id);

        Task<Item?> GetItemByNameAsync(string name);

        Task<List<Item>> GetItemsAsync();

        Task UpdateItemByIdAsync(Guid id, Item item);
    }
}
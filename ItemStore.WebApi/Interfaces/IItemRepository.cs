using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItems();

        Task<Item?> GetItemById(Guid id);

        Task<Item?> GetItemByName(string name);

        Task<Guid> AddItem(Item item);

        Task UpdateItemById(Guid id, Item updatedItem);

        Task DeleteItemById(Guid id);
    }
}

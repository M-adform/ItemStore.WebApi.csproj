using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;

namespace ItemStore.WebApi.Interfaces
{
    public interface IItemService
    {
        Task<List<GetItemResponse>> GetItems();

        Task<GetItemResponse?> GetItemById(Guid id);

        Task<Guid> AddItem(AddItemRequest item);

        Task UpdateItemById(Guid id, UpdateItemRequest updatedItem);

        Task DeleteItemById(Guid id);
    }
}

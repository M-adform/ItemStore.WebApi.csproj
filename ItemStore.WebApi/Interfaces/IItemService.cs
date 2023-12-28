using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;

namespace ItemStore.WebApi.Services
{
    public interface IItemService
    {
        Task<Guid> AddItem(AddItemRequest item);
        Task DeleteItemById(Guid id);
        Task<GetItemResponse?> GetItemById(Guid id);
        Task<List<GetItemResponse>> GetItems();
        Task UpdateItemById(Guid id, UpdateItemRequest updateRequest);
    }
}
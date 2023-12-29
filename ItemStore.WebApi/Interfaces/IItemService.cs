using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.Models.Entities;

namespace ItemStore.WebApi.Services
{
    public interface IItemService
    {
        Task<Item> AddItem(AddItemRequest item);

        Task DeleteItemById(Guid id);

        Task<GetItemResponse?> GetItemById(Guid id);

        Task<List<GetItemResponse>> GetItems();

        Task UpdateItemById(Guid id, UpdateItemRequest updateRequest);

        Task AddItemToShopByIdAsync(Guid id, AddItemToShopRequest request);

        Task DeleteItemFromShopByIdAsync(Guid id);
    }
}
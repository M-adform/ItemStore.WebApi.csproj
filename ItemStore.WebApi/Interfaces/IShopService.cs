using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ShopStore.WebApi.csproj.Services
{
    public interface IShopService
    {
        Task<int> AddShopAsync(AddShopRequest request);
        Task DeleteShopByIdAsync(int id);
        Task<Shop?> GetShopByIdAsync(int id);
        Task<List<Shop>> GetShopsAsync();
        Task UpdateShopByIdAsync(int id, UpdateShopRequest request);
    }
}
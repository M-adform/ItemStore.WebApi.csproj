using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ShopStore.WebApi.csproj.Services
{
    public interface IShopService
    {
        Task<Shop> AddShopAsync(AddShopRequest request);

        Task DeleteShopByIdAsync(int id);

        Task<List<GetShopResponse>> GetShopsAsync();

        Task<GetShopResponse> GetShopByIdAsync(int id);

        Task UpdateShopByIdAsync(int id, UpdateShopRequest request);
    }
}
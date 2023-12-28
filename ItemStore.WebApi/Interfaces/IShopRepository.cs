using ItemStore.WebApi.csproj.Models.Entities;

namespace ItemStore.WebApi.csproj.Interfaces
{
    public interface IShopRepository
    {
        Task<int> AddShopAsync(Shop shop);
        Task DeleteShopByIdAsync(int id);
        Task<Shop?> GetShopByIdAsync(int id);
        Task<Shop?> GetShopByNameAsync(string name);
        Task<List<Shop>> GetShopsAsync();
        Task UpdateShopByIdAsync(int id, Shop shop);
    }
}
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ItemStore.WebApi.csproj.Interfaces
{
    public interface IUserService
    {
        Task<List<User>?> GetUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task<User> AddUserAsync(AddUserRequest request);

        Task BuyItem(int userId, Guid itemId);
    }
}
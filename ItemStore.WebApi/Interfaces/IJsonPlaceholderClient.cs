using ItemStore.WebApi.csproj.Clients;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.csproj.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        Task<JsonPlaceholderResult<User>> AddUserAsync([FromBody] AddUserRequest addUserRequest);

        Task<JsonPlaceholderResult<User>> GetUserByIdAsync(int id);

        Task<JsonPlaceholderResult<List<User>>> GetUsersAsync();
    }
}
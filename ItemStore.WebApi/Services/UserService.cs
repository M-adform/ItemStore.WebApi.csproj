using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ItemStore.WebApi.csproj.Services
{
    public class UserService : IUserService
    {
        private readonly IJsonPlaceholderClient _client;
        private readonly IMapper _mapper;

        public UserService(IJsonPlaceholderClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<List<User>?> GetUsersAsync()
        {
            var result = await _client.GetUsersAsync();
            if (!result.IsSuccessful)
                throw new Exception("Users not retrieved.");

            return result.Data;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var result = await _client.GetUserByIdAsync(id);
            if (!result.IsSuccessful)
                throw new NotFoundException("User not found.");

            var user = _mapper.Map<User>(result.Data);
            return user;
        }

        public async Task<User> AddUserAsync(AddUserRequest request)
        {
            var result = await _client.AddUserAsync(request);
            if (!result.IsSuccessful)
                throw new Exception("User not added.");

            var user = _mapper.Map<User>(result.Data);
            return user;
        }
    }
}

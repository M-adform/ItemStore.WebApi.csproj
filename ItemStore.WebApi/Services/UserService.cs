using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;
using ItemStore.WebApi.Repositories;

namespace ItemStore.WebApi.csproj.Services
{
    public class UserService : IUserService
    {
        private readonly IJsonPlaceholderClient _client;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly IShopRepository _shopRepository;

        public UserService(IJsonPlaceholderClient client, IMapper mapper, IItemRepository itemRepository, IPurchaseHistoryRepository purchaseHistoryRepository, IShopRepository shopRepository)
        {
            _client = client;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _shopRepository = shopRepository;
        }

        public async Task<List<User>?> GetUsersAsync()
        {
            var usersResult = await _client.GetUsersAsync();
            if (!usersResult.IsSuccessful)
                throw new Exception("Users not retrieved.");

            return usersResult.Data;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var usersResult = await _client.GetUserByIdAsync(id);
            if (!usersResult.IsSuccessful)
                throw new NotFoundException("User not found.");

            return _mapper.Map<User>(usersResult.Data);
        }

        public async Task<User> AddUserAsync(AddUserRequest request)
        {
            var usersResult = await _client.AddUserAsync(request);
            if (!usersResult.IsSuccessful)
                throw new Exception("User not added.");

            return _mapper.Map<User>(usersResult.Data);
        }

        public async Task BuyItem(int userId, Guid itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId) ?? throw new NotFoundException("Item not found.");
            _ = await _shopRepository.GetShopByIdAsync(item.ShopId) ?? throw new NotFoundException("Item is not sold in shops.");
            var user = await _client.GetUserByIdAsync(userId);
            if (!user.IsSuccessful)
                throw new NotFoundException("User not found.");

            PurchaseHistory newPurchase = new()
            {
                UserId = userId,
                ItemId = itemId,
                ItemName = item.Name,
                Price = item.Price
            };

            item.OutOfStock = true;

            await _itemRepository.UpdateItemByIdAsync(itemId, item);
            await _purchaseHistoryRepository.BuyItem(newPurchase);
        }
    }
}
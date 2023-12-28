using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ShopStore.WebApi.csproj.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public ShopService(IShopRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<int> AddShopAsync(AddShopRequest request)
        {
            var existingShop = await _shopRepository.GetShopByNameAsync(request.Name);
            if (existingShop != null)
                throw new DuplicateValueException("Shop with this name already exists.");

            var shop = _mapper.Map<Shop>(existingShop);
            var id = await _shopRepository.AddShopAsync(shop);
            return id;
        }

        public async Task<List<Shop>> GetShopsAsync()
        {
            var shops = await _shopRepository.GetShopsAsync();
            var shopsToReturn = shops.Select(shop => _mapper.Map<Shop>(shop)).ToList();
            return shopsToReturn;
        }

        public async Task<Shop?> GetShopByIdAsync(int id)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id) ?? throw new NotFoundException("Shop not found.");
            var shopToReturn = _mapper.Map<Shop>(shop);
            return shopToReturn;
        }

        public async Task UpdateShopByIdAsync(int id, UpdateShopRequest request)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id) ?? throw new NotFoundException("Shop not found.");

            if (shop.Name != request.Name)
            {
                var shopWithSameName = await _shopRepository.GetShopByNameAsync(request.Name);

                if (shopWithSameName != null)
                    throw new DuplicateValueException("Shop with name already exists.");
            }

            var shopType = _mapper.Map<Shop>(request);
            await _shopRepository.UpdateShopByIdAsync(id, shopType);
        }

        public async Task DeleteShopByIdAsync(int id)
        {
            _ = await _shopRepository.GetShopByIdAsync(id) ?? throw new NotFoundException("Shop not found.");
            await _shopRepository.DeleteShopByIdAsync(id);
        }
    }
}
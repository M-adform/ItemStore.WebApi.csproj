using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.DTOs.ResponseDTOs;
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

        public async Task<Shop> AddShopAsync(AddShopRequest request)
        {
            var shop = await _shopRepository.GetShopByNameAsync(request.Name);
            if (shop != null)
                throw new DuplicateValueException("Shop with this name already exists.");

            var newShop = _mapper.Map<Shop>(request);
            return await _shopRepository.AddShopAsync(newShop);
        }

        public async Task<List<GetShopResponse>> GetShopsAsync()
        {
            var shops = await _shopRepository.GetShopsAsync();
            return shops.Select(shop => _mapper.Map<GetShopResponse>(shop)).ToList();
        }

        public async Task<GetShopResponse> GetShopByIdAsync(int id)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id) ?? throw new NotFoundException("Shop not found.");
            return _mapper.Map<GetShopResponse>(shop);
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

            var updatedShop = _mapper.Map<Shop>(request);
            await _shopRepository.UpdateShopByIdAsync(id, updatedShop);
        }

        public async Task DeleteShopByIdAsync(int id)
        {
            _ = await _shopRepository.GetShopByIdAsync(id) ?? throw new NotFoundException("Shop not found.");
            await _shopRepository.DeleteShopByIdAsync(id);
        }
    }
}
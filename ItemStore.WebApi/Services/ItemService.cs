using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Repositories;
using System.Data;


namespace ItemStore.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IShopRepository shopRepository)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        public async Task<List<GetItemResponse>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            return items.Select(item => _mapper.Map<GetItemResponse>(item)).ToList();
        }

        public async Task<GetItemResponse?> GetItemById(Guid id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");
            return _mapper.Map<GetItemResponse>(item);
        }

        public async Task<Item> AddItem(AddItemRequest request)
        {
            var item = await _itemRepository.GetItemByNameAsync(request.Name);
            if (item != null)
                throw new DuplicateValueException("Item with this name already exists.");

            var response = _mapper.Map<Item>(request);
            return await _itemRepository.AddItemAsync(response);
        }

        public async Task UpdateItemById(Guid id, UpdateItemRequest request)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");

            if (item.OutOfStock)
                throw new NotFoundException("Item out of stock.");

            if (item.Name != request.Name)
            {
                var duplicateNameItem = await _itemRepository.GetItemByNameAsync(request.Name);
                if (duplicateNameItem != null)
                    throw new DuplicateValueException("Item with name already exists.");
            }

            var response = _mapper.Map<Item>(request);
            await _itemRepository.UpdateItemByIdAsync(id, response);
        }

        public async Task DeleteItemById(Guid id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");

            if (item.OutOfStock)
                throw new NotFoundException("Item out of stock.");

            await _itemRepository.DeleteItemByIdAsync(id);
        }

        public async Task AddItemToShopByIdAsync(Guid id, AddItemToShopRequest request)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");
            if (item.OutOfStock)
                throw new NotFoundException("Item out of stock.");

            _ = await _shopRepository.GetShopByIdAsync(request.ShopId) ?? throw new NotFoundException("Shop not found.");

            item.ShopId = request.ShopId;
            await _itemRepository.UpdateItemByIdAsync(id, item);
        }

        public async Task DeleteItemFromShopByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");
            if (item.OutOfStock)
                throw new NotFoundException("Item out of stock.");

            _ = await _shopRepository.GetShopByIdAsync(item.ShopId) ?? throw new NotFoundException("Item is not assigned to a shop.");

            item.ShopId = null;
            await _itemRepository.UpdateItemByIdAsync(id, item);
        }
    }
}
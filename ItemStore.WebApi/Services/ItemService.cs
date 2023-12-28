using AutoMapper;
using ItemStore.WebApi.csproj.Exceptions;
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

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<GetItemResponse>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            var itemsToReturn = items.Select(item => _mapper.Map<GetItemResponse>(item)).ToList();
            return itemsToReturn;
        }

        public async Task<GetItemResponse?> GetItemById(Guid id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");
            var itemToReturn = _mapper.Map<GetItemResponse>(item);
            return itemToReturn;
        }

        public async Task<Guid> AddItem(AddItemRequest item)
        {
            var existingItem = await _itemRepository.GetItemByNameAsync(item.Name);
            if (existingItem != null)
                throw new DuplicateValueException("Item with this name already exists.");

            var itemType = _mapper.Map<Item>(item);
            var itemId = await _itemRepository.AddItemAsync(itemType);
            return itemId;
        }

        public async Task UpdateItemById(Guid id, UpdateItemRequest updateRequest)
        {
            var item = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");

            if (item.Name != updateRequest.Name)
            {
                var itemWithSameName = await _itemRepository.GetItemByNameAsync(updateRequest.Name);

                if (itemWithSameName != null)
                    throw new DuplicateValueException("Item with name already exists.");
            }

            var itemType = _mapper.Map<Item>(updateRequest);
            await _itemRepository.UpdateItemByIdAsync(id, itemType);
        }

        public async Task DeleteItemById(Guid id)
        {
            _ = await _itemRepository.GetItemByIdAsync(id) ?? throw new NotFoundException("Item not found.");
            await _itemRepository.DeleteItemByIdAsync(id);
        }
    }
}

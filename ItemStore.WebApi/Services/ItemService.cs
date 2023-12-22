using AutoMapper;
using ItemStore.WebApi.csproj.Helpers;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.ResponseDTOs;
using ItemStore.WebApi.Models.Entities;

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
            var items = await _itemRepository.GetItems();
            var itemsToReturn = items.Select(item => _mapper.Map<GetItemResponse>(item)).ToList();

            return itemsToReturn;
        }

        public async Task<GetItemResponse?> GetItemById(Guid id)
        {
            var item = await _itemRepository.GetItemById(id) ?? throw new ItemNotFoundException("Item not found.");
            var itemToReturn = _mapper.Map<GetItemResponse>(item);

            return itemToReturn;
        }

        public async Task<Guid> AddItem(AddItemRequest item)
        {
            var existingItem = await _itemRepository.GetItemByName(item.Name);
            if (existingItem != null)
                throw new DuplicateValueException("Item with this name already exists.");

            var itemType = _mapper.Map<Item>(item);
            var itemId = await _itemRepository.AddItem(itemType);

            return itemId;
        }

        public async Task UpdateItemById(Guid id, UpdateItemRequest updateRequest)
        {
            var item = await _itemRepository.GetItemById(id) ?? throw new ItemNotFoundException("Item not found.");

            if (item.Name != updateRequest.Name)
            {
                var itemWithSameName = await _itemRepository.GetItemByName(updateRequest.Name);
                if (itemWithSameName != null)
                    throw new DuplicateValueException("Item with name already exists.");
            }

            var itemType = _mapper.Map<Item>(updateRequest);
            await _itemRepository.UpdateItemById(id, itemType);
        }

        public async Task DeleteItemById(Guid id)
        {
            _ = await _itemRepository.GetItemById(id) ?? throw new ItemNotFoundException("Item not found.");
            await _itemRepository.DeleteItemById(id);
        }

        //public decimal GetItemPrice(Guid itemId, int quantity)
        //{
        //    var unitPrice = _itemRepository.GetItemById(itemId).Price;
        //    var price = unitPrice;

        //    if (quantity > 10)
        //    {
        //        return price * 0.9M;
        //    }
        //    else if (quantity > 20)
        //    {
        //        return price * 0.8M;
        //    }
        //    else
        //    {
        //        return price;
        //    }
        //}
    }
}

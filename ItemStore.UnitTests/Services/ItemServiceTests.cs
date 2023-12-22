using AutoMapper;
using ItemStore.WebApi.csproj.Helpers;
using ItemStore.WebApi.csproj.Mappings;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Services;
using Moq;

namespace ItemStore.UnitTests.Services
{
    public class ItemServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly IItemService _itemService;

        public ItemServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
            _itemRepository = new Mock<IItemRepository>();
            _itemService = new ItemService(_itemRepository.Object, _mapper);
        }

        [Fact]
        public async Task Get_ReturnsDtoList()
        {
            var testItems = new List<Item>
            {
            new() { Id= Guid.Parse("45eda11c-8984-4f46-9bbf-a5d91de0ffdf"),Name = "Item1", Price=1 },
            new() { Id =  Guid.Parse("45eda11c-8984-4f46-9bbf-a5d91de0faaa"),Name = "Item2", Price = 5 }
            };

            _itemRepository.Setup(repo => repo.GetItems())
              .ReturnsAsync(testItems);

            var result = await _itemService.GetItems();

            Assert.Equal(testItems.Count, result.Count);
            Assert.Equal(testItems[0].Name, result[0].Name);
            Assert.Equal(testItems[0].Price, result[0].Price);
        }

        [Fact]
        public async Task GetItems_GivenValidId_ReturnsDto()
        {
            Item item = new Item();
            var itemId = new Guid();

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                          .ReturnsAsync(item);

            var result = await _itemService.GetItemById(itemId);

            Assert.Equal(item.Id, itemId);
        }

        [Fact]
        public async Task GetItemById_GivenInvalidId_ThrowsItemNotFoundException()
        {
            var itemId = new Guid();

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                         .ReturnsAsync((Item?)null);

            var exception = await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.GetItemById(itemId));
            Assert.Equal("Item not found.", exception.Message);
        }

        [Fact]
        public async Task AddItem_NameIsUnique_SuccessfullyAddsItem()
        {
            AddItemRequest addItemRequest = new AddItemRequest();

            var expectedId = new Guid();

            _itemRepository.Setup(repo => repo.GetItemByName(addItemRequest.Name))
                          .ReturnsAsync((Item?)null);

            _itemRepository.Setup(repo => repo.AddItem(It.IsAny<Item>()))
                          .ReturnsAsync(new Guid());

            var itemId = await _itemService.AddItem(addItemRequest);

            _itemRepository.Verify(repo => repo.GetItemByName(addItemRequest.Name), Times.Once);
            _itemRepository.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Once);
            Assert.Equal(expectedId, itemId);
        }

        [Fact]
        public async Task AddItem_NameIsNotUnique_ThrowsDuplicateValueException()
        {
            AddItemRequest addItemRequest = new AddItemRequest { Name = "Item", Price = 2 };

            _itemRepository.Setup(repo => repo.GetItemByName("Name"))
                          .ReturnsAsync(new Item());

            _itemRepository.Setup(repo => repo.AddItem(It.IsAny<Item>()))
                   .Callback((Item item) => throw new DuplicateValueException("Item with this name already exists."));

            await Assert.ThrowsAsync<DuplicateValueException>(async () => await _itemService.AddItem(addItemRequest));
        }

        [Fact]
        public async Task UpdateItemById_GivenValidId_SuccessfullyUpdatesItem()
        {
            var itemId = new Guid();
            UpdateItemRequest updateRequest = new UpdateItemRequest { Name = "UpdatedItem", Price = 2 };

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                          .ReturnsAsync(new Item());

            _itemRepository.Setup(repo => repo.UpdateItemById(itemId, It.IsAny<Item>()));

            _itemRepository.Setup(repo => repo.GetItemByName(updateRequest.Name))
                          .ReturnsAsync((Item?)null);

            await _itemService.UpdateItemById(itemId, updateRequest);

            _itemRepository.Verify(repo => repo.UpdateItemById(itemId, It.IsAny<Item>()), Times.Once);
            _itemRepository.Verify(repo => repo.GetItemByName(updateRequest.Name), Times.Once);
        }

        [Fact]
        public async Task UpdateItemById_GivenInvalidId_ThrowsItemNotFoundException()
        {
            var itemId = new Guid();
            UpdateItemRequest updateRequest = new UpdateItemRequest();

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                          .ReturnsAsync((Item?)null);

            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.UpdateItemById(itemId, updateRequest));

            _itemRepository.Verify(repo => repo.GetItemById(itemId), Times.Once);
        }

        [Fact]
        public async Task DeleteItemById_GivenValidId_SuccessfullyDeletesItem()
        {
            var itemId = new Guid();

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                          .ReturnsAsync(new Item());

            await _itemService.DeleteItemById(itemId);

            _itemRepository.Verify(repo => repo.GetItemById(itemId), Times.Once);
            _itemRepository.Verify(repo => repo.DeleteItemById(itemId), Times.Once);
        }

        [Fact]
        public async Task DeleteItemById_GivenInvalidId_ThrowsItemNotFoundException()
        {
            var itemId = new Guid();

            _itemRepository.Setup(repo => repo.GetItemById(itemId))
                          .ReturnsAsync((Item?)null);

            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemService.DeleteItemById(itemId));

            _itemRepository.Verify(repo => repo.DeleteItemById(It.IsAny<Guid>()), Times.Never);
        }

    }
}
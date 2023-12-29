using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Mappings;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.Entities;
using ItemStore.WebApi.Repositories;
using ItemStore.WebApi.Services;
using Moq;

namespace ItemStore.UnitTests.Services
{
    public class ItemServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<IShopRepository> _shopRepositoryMock;
        private readonly IItemService _itemService;
        private readonly Fixture _fixture;

        public ItemServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
            _itemRepositoryMock = new Mock<IItemRepository>();
            _shopRepositoryMock = new Mock<IShopRepository>();
            _itemService = new ItemService(_itemRepositoryMock.Object, _mapper, _shopRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Get_TwoItemsExist_ReturnsDtoList()
        {
            var testItems = new List<Item>
            {
            new() { Id= Guid.Parse("45eda11c-8984-4f46-9bbf-a5d91de0ffdf"),Name = "Item1", Price=1 },
            new() { Id =  Guid.Parse("45eda11c-8984-4f46-9bbf-a5d91de0faaa"),Name = "Item2", Price = 5 }
            };

            const int ITEM_COUNT = 2;

            _itemRepositoryMock.Setup(repo => repo.GetItemsAsync())
              .ReturnsAsync(testItems);

            var result = await _itemService.GetItems();

            _itemRepositoryMock.Verify(repo => repo.GetItemsAsync(), Times.Once);

            result.Should().NotBeNull();
            result.Should().HaveCount(ITEM_COUNT);

            result[0].Name.Should().BeEquivalentTo(testItems[0].Name);
            result[1].Name.Should().BeEquivalentTo(testItems[1].Name);

            result[0].Price.Should().Be(testItems[0].Price);
            result[1].Price.Should().Be(testItems[1].Price);
        }

        [Theory]
        [AutoData]
        public async Task GetItemById_ItemExists_ReturnsDto(Guid id)
        {
            Item item = new() { Id = id, Name = "Tomato" };

            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                          .ReturnsAsync(item);

            var result = await _itemService.GetItemById(id);

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);

            result.Should().NotBeNull();
            result.Name.Should().Be(item.Name);
        }

        [Theory]
        [AutoData]
        public async Task GetItemById_NonexistentItem_ThrowsItemNotFoundException(Guid id)
        {
            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                         .ReturnsAsync((Item?)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _itemService.GetItemById(id));

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Item not found.");
        }

        //[Theory]
        //[AutoData]
        //public async Task AddItem_NameIsUnique_SuccessfullyAddsItem(AddItemRequest addItemRequest)
        //{
        //    var id = _fixture.Create<Guid>();

        //    _itemRepositoryMock.Setup(repo => repo.GetItemByNameAsync(addItemRequest.Name))
        //                  .ReturnsAsync((Item?)null);

        //    _itemRepositoryMock.Setup(repo => repo.AddItemAsync(It.Is<Item>(item => item.Name == addItemRequest.Name && item.Price == addItemRequest.Price)))
        //                    .ReturnsAsync(id);

        //    var itemId = await _itemService.AddItem(addItemRequest);

        //    _itemRepositoryMock.Verify(repo => repo.GetItemByNameAsync(addItemRequest.Name), Times.Once);
        //    _itemRepositoryMock.Verify(repo => repo.AddItemAsync(It.Is<Item>(item => item.Name == addItemRequest.Name && item.Price == addItemRequest.Price)), Times.Once);

        //    itemId.Should().NotBe(Guid.Empty);
        //    itemId.Should().Be(id);
        //}

        [Theory]
        [AutoData]
        public async Task AddItem_NameIsNotUnique_ThrowsDuplicateValueException(AddItemRequest addRequest)
        {
            _itemRepositoryMock.Setup(repo => repo.GetItemByNameAsync(addRequest.Name))
                          .ReturnsAsync(new Item { Name = addRequest.Name });

            var exception = await Assert.ThrowsAsync<DuplicateValueException>(async () => await _itemService.AddItem(addRequest));

            _itemRepositoryMock.Verify(repo => repo.GetItemByNameAsync(addRequest.Name), Times.Once);

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Item with this name already exists.");
        }

        [Theory]
        [AutoData]
        public async Task UpdateItemById_ItemExists_SuccessfullyUpdatesItem(Guid id, UpdateItemRequest updateRequest)
        {
            Item item = new() { Id = id, Name = "Name", Price = 5 };
            Item updatedItem = new() { Id = id, Name = updateRequest.Name, Price = updateRequest.Price };
            updateRequest.Id = id;

            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                          .ReturnsAsync(item);

            _itemRepositoryMock.Setup(repo => repo.GetItemByNameAsync(updateRequest.Name))
                          .ReturnsAsync((Item?)null);

            _itemRepositoryMock.Setup(repo => repo.UpdateItemByIdAsync(id, It.Is<Item>(item => item.Id == id && item.Name == updateRequest.Name && item.Price == updateRequest.Price)));

            await _itemService.UpdateItemById(id, updateRequest);

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);
            _itemRepositoryMock.Verify(repo => repo.GetItemByNameAsync(updateRequest.Name), Times.Once);
            _itemRepositoryMock.Verify(repo => repo.UpdateItemByIdAsync(id, It.Is<Item>(item => item.Id == id && item.Name == updateRequest.Name && item.Price == updateRequest.Price)), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task UpdateItemById_NonexistentItem_ThrowsItemNotFoundException(Guid id, UpdateItemRequest updateRequest)
        {
            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                            .ReturnsAsync((Item?)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _itemService.UpdateItemById(id, updateRequest));

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Item not found.");
        }

        [Theory]
        [AutoData]
        public async Task DeleteItemById_ItemExists_SuccessfullyDeletesItem(Guid id)
        {
            Item item = new() { Id = id, };

            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                          .ReturnsAsync(item);

            await _itemService.DeleteItemById(id);

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);
            _itemRepositoryMock.Verify(repo => repo.DeleteItemByIdAsync(id), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteItemById_NonexistentItem_ThrowsItemNotFoundException(Guid id)
        {
            _itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(id))
                          .ReturnsAsync((Item?)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _itemService.DeleteItemById(id));

            _itemRepositoryMock.Verify(repo => repo.GetItemByIdAsync(id), Times.Once);
            _itemRepositoryMock.Verify(repo => repo.DeleteItemByIdAsync(id), Times.Never);

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Item not found.");
        }
    }
}
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using ItemStore.WebApi.csproj.Clients;
using ItemStore.WebApi.csproj.Exceptions;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Mappings;
using ItemStore.WebApi.csproj.Models.Entities;
using ItemStore.WebApi.csproj.Services;
using Moq;

namespace ItemStore.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IJsonPlaceholderClient> _clientMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
            _clientMock = new Mock<IJsonPlaceholderClient>();
            _userService = new UserService(_clientMock.Object, _mapper);
        }

        [Theory]
        [AutoData]
        public async Task GetUserById_UserExists_ReturnsUser(int id)
        {
            var clientResult = new JsonPlaceholderResult<User>() { Data = new User() { Id = id }, IsSuccessful = true };

            _clientMock.Setup(client => client.GetUserByIdAsync(id))
                         .ReturnsAsync(clientResult);

            var user = await _userService.GetUserByIdAsync(id);

            _clientMock.Verify(client => client.GetUserByIdAsync(id), Times.Once);

            user.Should().NotBeNull();
            user.Id.Should().Be(id);
        }

        [Theory]
        [AutoData]
        public async Task GetUserById_NonexistentUser_ThrowsUserNotFoundException(int id)
        {
            var clientResult = new JsonPlaceholderResult<User>() { IsSuccessful = false };

            _clientMock.Setup(client => client.GetUserByIdAsync(id))
                         .ReturnsAsync(clientResult);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserByIdAsync(id));

            _clientMock.Verify(client => client.GetUserByIdAsync(id), Times.Once);

            exception.Should().NotBeNull();
            exception.Message.Should().Be("User not found.");
        }
    }
}

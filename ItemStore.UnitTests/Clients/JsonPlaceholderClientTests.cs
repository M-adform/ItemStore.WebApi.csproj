using AutoFixture.Xunit2;
using FluentAssertions;
using ItemStore.WebApi.csproj.Clients;
using ItemStore.WebApi.csproj.Models.Entities;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;

namespace ItemStore.UnitTests.UserClientTests
{
    public class JsonPlaceholderClientTests
    {
        private const string BASE_URL = "https://jsonplaceholder.typicode.com";

        [Theory]
        [AutoData]
        public async Task GetUserById_UserExists_ReturnsDto(int id)
        {
            var user = new User() { Id = id, Username = "Sincereapril" };

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When($"{BASE_URL}/users/{id}")
                    .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(user));

            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            var jsonPlaceholderClient = new JsonPlaceholderClient(client);

            var result = await jsonPlaceholderClient.GetUserByIdAsync(id);
            var count = mockHttp.GetMatchCount(request);

            count.Should().Be(1);
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Username.Should().Be(user.Username);
            result.Data.Id.Should().Be(user.Id);
            result.ErrorMessage.Should().BeEmpty();
        }

        // FIX THIS ------>
        [Theory]
        [AutoData]
        public async Task GetUserById_NonexistentUser_ReturnsDto(int id)
        {
            var response = new JsonPlaceholderResult<User>() { IsSuccessful = false, ErrorMessage = "Error occured.", Data = { } };

            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When($"{BASE_URL}/users/{id}")
                    .Respond("application/json", JsonConvert.SerializeObject(response));

            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            var jsonPlaceholderClient = new JsonPlaceholderClient(client);

            // Find a way to mock status code.
            var result = await jsonPlaceholderClient.GetUserByIdAsync(id);
            var count = mockHttp.GetMatchCount(request);

            count.Should().Be(1);
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.ErrorMessage.Should().Be("Error occured.");
            result.Data.Should().BeNull();
        }
    }
}
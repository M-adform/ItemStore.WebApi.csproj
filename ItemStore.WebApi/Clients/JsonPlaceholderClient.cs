using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.csproj.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ItemStore.WebApi.csproj.Clients
{
    public class JsonPlaceholderClient : IJsonPlaceholderClient
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderClient(HttpClient client)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        }

        public async Task<JsonPlaceholderResult<List<User>>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("/users");
            if (!response.IsSuccessStatusCode)
                return new JsonPlaceholderResult<List<User>> { IsSuccessful = false, ErrorMessage = response.StatusCode.ToString() };

            var users = await response.Content.ReadAsAsync<List<User>>();
            return new JsonPlaceholderResult<List<User>> { IsSuccessful = true, ErrorMessage = "", Data = users };
        }

        public async Task<JsonPlaceholderResult<User>> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/users/{id}");
            if (!response.IsSuccessStatusCode)
                return new JsonPlaceholderResult<User> { IsSuccessful = false, ErrorMessage = response.ReasonPhrase };

            var data = await response.Content.ReadAsAsync<User>();
            return new JsonPlaceholderResult<User> { IsSuccessful = true, ErrorMessage = "", Data = data };
        }

        public async Task<JsonPlaceholderResult<User>> AddUserAsync([FromBody] AddUserRequest addUserRequest)
        {
            string jsonContent = JsonSerializer.Serialize(addUserRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/users", content);
            if (!response.IsSuccessStatusCode)
                return new JsonPlaceholderResult<User> { IsSuccessful = false, ErrorMessage = response.StatusCode.ToString() };

            var data = await response.Content.ReadAsAsync<User>();
            return new JsonPlaceholderResult<User> { IsSuccessful = true, ErrorMessage = "", Data = data };
        }
    }
}
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Mvc;


namespace ItemStore.WebApi.csproj.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            var createdUser = await _userService.AddUserAsync(request);
            return CreatedAtAction("GetUserById", new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> BuyItem(int id)
        {
            return Ok();
        }

    }
}

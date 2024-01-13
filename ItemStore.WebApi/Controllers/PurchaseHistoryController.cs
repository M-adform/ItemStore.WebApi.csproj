using ItemStore.WebApi.csproj.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.csproj.Controllers
{
    [ApiController]
    [Route("order")]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly IUserService _userService;

        public PurchaseHistoryController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> BuyItem(int userId, Guid id)
        {
            await _userService.BuyItem(userId, id);
            return NoContent();
        }
    }
}

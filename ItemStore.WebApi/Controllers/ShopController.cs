using Microsoft.AspNetCore.Mvc;
using ShopStore.WebApi.csproj.Services;

namespace ItemStore.WebApi.csproj.Controllers
{
    [ApiController]
    [Route("shop")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddShopAsync(AddShopRequest request)
        //{
        //    var newShopId = await _shopService.AddShopAsync(request);
        //    return CreatedAtAction(nameof(GetShopByIdAsync), new { id = newShopId }, request);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopAsync(int id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopByIdAsync(int id)
        {
            return Ok();
        }
    }
}

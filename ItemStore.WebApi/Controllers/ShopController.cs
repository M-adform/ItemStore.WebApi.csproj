using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetShops()
        {
            return Ok(await _shopService.GetShopsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById(int id)
        {
            return Ok(await _shopService.GetShopByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddShop(AddShopRequest request)
        {
            var addedShop = await _shopService.AddShopAsync(request);
            return CreatedAtAction(nameof(GetShopById), new { id = addedShop.Id }, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            await _shopService.DeleteShopByIdAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopById(int id, UpdateShopRequest request)
        {
            await _shopService.UpdateShopByIdAsync(id, request);
            return Ok();
        }
    }
}
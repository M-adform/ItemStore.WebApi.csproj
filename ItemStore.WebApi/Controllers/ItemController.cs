using ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Models.DTOs.RequestDTOs;
using ItemStore.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.WebApi.Controllers
{
    [ApiController]
    [Route("item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await _itemService.GetItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            return Ok(await _itemService.GetItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
        {
            var addedItem = await _itemService.AddItem(request);
            return CreatedAtAction(nameof(GetItemById), new { id = addedItem.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemById(Guid id, [FromBody] UpdateItemRequest request)
        {
            await _itemService.UpdateItemById(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemById(Guid id)
        {
            await _itemService.DeleteItemById(id);
            return NoContent();
        }

        [HttpPut("{id}/add-to-shop")]
        public async Task<IActionResult> AddItemToShopById(Guid id, [FromBody] AddItemToShopRequest request)
        {
            await _itemService.AddItemToShopByIdAsync(id, request);
            return NoContent();
        }

        [HttpPut("{id}/remove-from-shop")]
        public async Task<IActionResult> DeleteItemFromShopById(Guid id)
        {
            await _itemService.DeleteItemFromShopByIdAsync(id);
            return NoContent();
        }
    }
}
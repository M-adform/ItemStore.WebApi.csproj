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
            var items = await _itemService.GetItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _itemService.GetItemById(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequest newItem)
        {
            var newItemId = await _itemService.AddItem(newItem);
            return CreatedAtAction(nameof(GetItemById), new { id = newItemId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemById(Guid id, [FromBody] UpdateItemRequest updatedItem)
        {
            await _itemService.UpdateItemById(id, updatedItem);
            return CreatedAtAction(nameof(GetItemById), new { id }, updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemById(Guid id)
        {
            await _itemService.DeleteItemById(id);
            return NoContent();
        }

        [HttpPut("{id}/add-to-shop")]
        public async Task<IActionResult> AddItemToShopByIdAsync(int id)
        {
            return Ok();
        }

        [HttpPut("{id}/remove-from-shop")]
        public async Task<IActionResult> DeleteItemFromShopByIdAsync(int id)
        {
            return Ok();
        }
    }
}

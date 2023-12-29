using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs
{
    public class AddItemToShopRequest
    {
        [Range(1, int.MaxValue)]
        public int ShopId { get; set; }
    }
}
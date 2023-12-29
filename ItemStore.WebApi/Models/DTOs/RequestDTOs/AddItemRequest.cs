using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.Models.DTOs.RequestDTOs
{
    public class AddItemRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}
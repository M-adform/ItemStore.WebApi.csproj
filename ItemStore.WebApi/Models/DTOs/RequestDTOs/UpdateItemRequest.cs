using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemStore.WebApi.Models.DTOs.RequestDTOs
{
    public class UpdateItemRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
    }
}
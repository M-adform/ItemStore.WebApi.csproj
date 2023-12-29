using System.ComponentModel.DataAnnotations;

namespace ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs
{
    public class UpdateShopRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
    }
}
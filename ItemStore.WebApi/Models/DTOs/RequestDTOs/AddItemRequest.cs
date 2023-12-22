using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.DTOs.RequestDTOs
{
    public class AddItemRequest
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}

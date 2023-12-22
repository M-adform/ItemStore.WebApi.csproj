using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.DTOs.ResponseDTOs
{
    public class GetItemResponse
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}

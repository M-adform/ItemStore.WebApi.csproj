using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ItemStore.WebApi.Models.DTOs.RequestDTOs
{
    public class UpdateItemRequest
    {
        [JsonIgnore]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace ItemStore.WebApi.csproj.Models.DTOs.RequestDTOs
{
    public class UpdateShopRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Adress { get; set; }
    }
}

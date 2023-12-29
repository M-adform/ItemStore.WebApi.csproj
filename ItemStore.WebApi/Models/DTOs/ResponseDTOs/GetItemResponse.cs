namespace ItemStore.WebApi.Models.DTOs.ResponseDTOs
{
    public class GetItemResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
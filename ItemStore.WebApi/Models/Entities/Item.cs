using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.Entities
{
    public class Item
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}

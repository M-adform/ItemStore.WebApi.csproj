using ItemStore.WebApi.csproj.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.Models.Entities
{
    [Table("items")]
    public class Item
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Column("out_of_stock")]
        public bool OutOfStock { get; set; } = false;

        [Column("shop_id")]
        public int? ShopId { get; set; }

        public Shop? Shop { get; set; }
    }
}
using ItemStore.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.csproj.Models.Entities
{
    [Table("purchase-histories")]
    public class PurchaseHistory
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("item_id")]
        public Guid ItemId { get; set; }

        public Item? Item { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("item_name")]
        public string ItemName { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("shop_name")]
        public string ShopName { get; set; }
    }
}
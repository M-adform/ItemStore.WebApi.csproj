using ItemStore.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemStore.WebApi.csproj.Models.Entities
{
    [Table("shop")]
    public class Shop
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("address")]
        public string Address { get; set; }

        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
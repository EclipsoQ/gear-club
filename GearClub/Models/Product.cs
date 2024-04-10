using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearClub.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int Warranty { get; set; }
        [MinLength(20)]
        public string? Description { get; set; }
        [StringLength(50)]
        public string Brand { get; set; } = null!;
        [StringLength(50)]
        public string Model { get; set; } = null!;
        public virtual ICollection<Category_Product> Category_Products { get; set; } = new List<Category_Product>();
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<CartDetail>? CartDetails { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<Specification>? Specifications { get; set; }
    }
}

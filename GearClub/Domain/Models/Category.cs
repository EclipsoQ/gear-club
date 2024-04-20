using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearClub.Domain.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Category_Product>? Category_Products { get; set; }
        public virtual Image? Image { get; set; }

    }
}

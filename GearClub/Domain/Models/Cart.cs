using GearClub.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearClub.Domain.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartDetail>? CartDetails { get; set; }

    }
}

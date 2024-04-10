using GearClub.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearClub.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Ward { get; set; } = null!;
        [MinLength(10)]
        public string Details { get; set; } = null!;
        public string Recipient { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Order>? Orders { get; set; } 
    }
}

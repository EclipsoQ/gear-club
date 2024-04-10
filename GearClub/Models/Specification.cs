using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearClub.Models
{
    public class Specification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecId { get; set; }        
        public string Name { get; set; } = null!;
        [MinLength(50)]
        public string Content { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;


    }
}

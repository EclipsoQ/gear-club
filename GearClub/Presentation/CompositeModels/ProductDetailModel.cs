using GearClub.Domain.Models;

namespace GearClub.Presentation.CompositeModels
{
    public class ProductDetailModel
    {
        public Product Product { get; set; } = null!;
        public ICollection<Specification> Specifications { get; set; } = new List<Specification>();
        public IEnumerable<Image>? DetailImages { get; set; }
    }
}

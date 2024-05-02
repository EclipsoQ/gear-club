using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GearClub.Presentation.CompositeModels
{
    public class FilterData
    {
       // public int CategoryId { get; set; }
        public List<string>? PriceRange { get; set; }
        public List<string>? Brand { get; set; }
    }
}

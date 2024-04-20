using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GearClub.Presentation.CompositeModels
{
    public class FilterData
    {
        public int CategoryId { get; set; }
        public string? PriceRange { get; set; }
        public string? Brand { get; set; }
    }
}

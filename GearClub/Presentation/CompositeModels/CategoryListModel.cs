using GearClub.Domain.Models;

namespace GearClub.Presentation.CompositeModels
{
    public class CategoryListModel
    {
        public Category? Category { get; set; }
        public int ProductsInCategory { get; set; }
        public Image? Image { get; set; }
    }
}

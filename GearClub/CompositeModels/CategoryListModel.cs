using GearClub.Models; 

namespace GearClub.CompositeModels
{
    public class CategoryListModel
    {
        public Category? Category { get; set; }
        public int ProductsInCategory { get; set; }
    }
}

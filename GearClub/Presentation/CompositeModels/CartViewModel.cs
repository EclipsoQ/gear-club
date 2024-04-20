using GearClub.Domain.Models;
using System.Security.Principal;

namespace GearClub.Presentation.CompositeModels
{
    public class CartViewModel
    {        
        public IEnumerable<CartDetail>? CartDetails { get; set; }
        public decimal Subtotals { get; set; }
        public int ShippingFee { get; set; }
        public decimal Total()
        {
            return Subtotals + ShippingFee;
        }
    }
}

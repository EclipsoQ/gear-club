using GearClub.Domain.Models;
using GearClub.Presentation.CompositeModels;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface ICartService
    {
        Cart GetCartByUser(string userId);        
        ICollection<Cart> GetAll();
        bool AddToCart(CartDetail cartDetail);
        bool RemoveLine(int cartLineId);
        bool UpdateLine(int cartLineId, int quantity);
        CartViewModel GetAllDetail(int cartId);
        bool CreateCart(string userId);
        decimal? ComputeLineSubtotal(int cartLineId);
        CartViewModel ComputeCartValues(int cartId);
        Cart GetCartByLine(int lineId);
        List<CartDetail> GetDetails(int cartId);
    }
}

using GearClub.Domain.Models;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface IOrderDetailService
    {               
        void CopyFromCartDetails(List<CartDetail> cartDetails);
    }
}

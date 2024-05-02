using GearClub.Domain.Models;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface IOrderService 
    {
        bool CreateOrder(Order order);
        bool CancelOrder(Order order);
        bool UpdateOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderDetail(int id);
        List<Order>? GetOrdersByUser(string userId);
    }
}

using GearClub.Domain.Models;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface IOrderService 
    {
        void CreateOrder(Order order);
        bool CancelOrder(int id);
        bool UpdateOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderDetail(int id);
        List<Order>? GetOrdersByUser(string userId);
        void ProcessOrder(Order order, Cart cart);
        Order? GetOrderById(int id);
    }
}

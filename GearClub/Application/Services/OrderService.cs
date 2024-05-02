using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;

namespace GearClub.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        public OrderService(IRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public bool CancelOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order>? GetOrdersByUser(string id)
        {
            var orders = _orderRepo.GetAll().Where(o => o.Address.UserId == id).ToList();
            return orders;
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

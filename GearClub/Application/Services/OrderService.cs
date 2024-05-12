using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GearClub.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<OrderDetail> _orderDetailRepo;
        private readonly IRepository<CartDetail> _cartDetailRepo;
        private readonly IRepository<Product> _producRepo;
        public OrderService(IRepository<Order> orderRepo, IRepository<Cart> cartRepo, 
            IRepository<OrderDetail> orderDetailRepo, IRepository<CartDetail> cartDetailRepo, 
            IRepository<Product> producRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _orderDetailRepo = orderDetailRepo;
            _cartDetailRepo = cartDetailRepo;
            _producRepo = producRepo;
        }
        public bool CancelOrder(int id)
        {
            Order order = _orderRepo.GetById(id);
            if (order == null)
            {
                return false;
            }

            order.Status = "Đã hủy";
            _orderRepo.Update(order);

            foreach(var line in order.OrderDetails)
            {
                var product = _producRepo.GetById(line.ProductId);
                if (product == null)
                {
                    return false;
                }
                product.StockQuantity += line.Quantity;
                _producRepo.Update(product);
            }

            return true;
        }

        public void CreateOrder(Order order)
        {
            try
            {                
                _orderRepo.Add(order);                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.GetAll().ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _orderRepo.GetById(id);
        }

        public Order GetOrderDetail(int id)
        {
            return _orderRepo.GetById(id);
        }

        public List<Order>? GetOrdersByUser(string id)
        {
            var orders = _orderRepo.GetAll().Where(o => o.Address.UserId == id).ToList();
            return orders;
        }

        public void ProcessOrder(Order order, Cart cart)
        {
            try
            {
                // Create new order
                CreateOrder(order);
                var orderId = GetOrdersByUser(cart.UserId)
                    .FirstOrDefault(o => o.OrderDate == order.OrderDate).OrderId;

                // Copy details from cart
                var details = _cartDetailRepo.GetAll().Where(cd => cd.CartId == cart.CartId);
                decimal subtotal = 0;
                foreach (var item in details)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        OrderId = orderId,                        
                    };
                    var product = _producRepo.GetById(item.ProductId);
                    product.StockQuantity -= item.Quantity;
                    _producRepo.Update(product);

                    subtotal += item.Quantity + item.Product.Price;
                    _orderDetailRepo.Add(orderDetail);
                }

                order.Subtotal = subtotal;
                _orderRepo.Update(order);

                cart.IsCheckedOut = true;
                _cartRepo.Update(cart);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateOrder(Order order)
        {
           try
           {
               _orderRepo.Update(order);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
        }
        
    }
}

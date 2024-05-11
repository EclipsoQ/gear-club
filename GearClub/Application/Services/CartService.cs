using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Presentation.CompositeModels;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<CartDetail> _cartDetailRepo;
        private readonly IRepository<Product> _productRepo;
        public CartService(IRepository<Cart> cartRepo, IRepository<CartDetail> cartDetailRepo, 
            IRepository<Product> productRepo)
        {
            _cartRepo = cartRepo;
            _cartDetailRepo = cartDetailRepo;
            _productRepo = productRepo;
        }
        public bool AddToCart(CartDetail line)
        {
            if (_productRepo.GetById(line.ProductId).StockQuantity == 0)
            {
                return false;
            }

            var lineToUpdate = _cartDetailRepo.GetAll().FirstOrDefault(c => c.ProductId == line.ProductId 
                && c.CartId == line.CartId);
            if (lineToUpdate == null)
            {
                try
                {
                    _cartDetailRepo.Add(line);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    lineToUpdate.Quantity += line.Quantity;
                    if(lineToUpdate.Quantity == 0)
                    {
                        lineToUpdate.Quantity = 1;
                    }
                    _cartDetailRepo.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
            
        }

        public decimal? ComputeLineSubtotal(int cartLineId)
        {
            var lineInfos = from lines in _cartDetailRepo.GetAll()
                           join products in _productRepo.GetAll()
                           on lines.ProductId equals products.ProductId
                           select lines;
            var lineToCompute = lineInfos.FirstOrDefault(c => c.CartDetailId == cartLineId);
            
            var subtotal = lineToCompute?.Quantity * lineToCompute?.Product?.Price;
            return subtotal;
        }

        public CartViewModel ComputeCartValues(int cartId)
        {
            var lines = from products in _productRepo.GetAll()
                        join cartLines in _cartDetailRepo.GetAll().Where(c => c.CartId == cartId)
                        on products.ProductId equals cartLines.ProductId                        
                        select cartLines;
            decimal subtotals = 0;
            foreach( var line in lines)
            {
                subtotals += line.Quantity * line.Product.Price;
            }
            var shippingFee = ComputeShippingFee(subtotals);
            return new CartViewModel
            {
                Subtotals = subtotals,
                ShippingFee = shippingFee
            };
        }

        public int ComputeShippingFee(decimal? total)
        {
            if (total <= 300000) return 15000;
            else if (total <= 500000) return 10000;
            else return 0;
        }

        public bool CreateCart(string userId)
        {
            try
            {
                _cartRepo.Add(new Cart
                {
                    UserId = userId,
                    IsCheckedOut = false
                });
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public ICollection<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetAllDetail(int cartId)
        {       
            CartViewModel model = new CartViewModel();
            model.CartDetails = from cartLines in _cartDetailRepo.GetAll().Where(c => c.CartId == cartId)
                        join products in _productRepo.GetAll()
                        on cartLines.ProductId equals products.ProductId
                        select cartLines;
            model.Subtotals = ComputeCartValues(cartId).Subtotals;
            model.ShippingFee = ComputeCartValues(cartId).ShippingFee;
            return model;
        }

        public Cart GetCartByUser(string userId)
        {
            return _cartRepo.GetAll().FirstOrDefault(c => c.UserId == userId && c.IsCheckedOut == false);
        }

        public bool RemoveLine(int cartLineId)
        {
            var line = _cartDetailRepo.GetById(cartLineId);
            try
            {
                _cartDetailRepo.Delete(line);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateLine(int cartLineId, int quantity)
        {
            var line = _cartDetailRepo.GetById(cartLineId);
            if (line == null)
            {
                return false;
            }
            
            line.Quantity += quantity;
            if (line.Quantity == 0)
            {
                line.Quantity = 1;                
            }
            _cartDetailRepo.Update(line);
            return true;
        }

        public Cart GetCartByLine(int lineId)
        {
            var line = _cartDetailRepo.GetById(lineId);
            return _cartRepo.GetById(line.CartId);
        }

        public List<CartDetail> GetDetails(int cartId)
        {
            return _cartDetailRepo.GetAll().Where(cd => cd.CartId == cartId).ToList();
        }
    }
}

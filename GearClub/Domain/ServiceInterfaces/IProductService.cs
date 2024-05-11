using GearClub.Domain.Models;
using GearClub.Presentation.CompositeModels;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface IProductService
    {
        ProductDetailModel GetProductDetail(int productId);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        bool DeleteProduct(Product product);
        bool UpdateProduct(int productId, Product product);
        IEnumerable<Product> SearchProduct(string? searchString);
        IEnumerable<Product> FilterProduct(FilterData filter);
    }
}

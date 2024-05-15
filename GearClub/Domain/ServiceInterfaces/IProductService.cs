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
        void CreateProduct(ProductViewModel model);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        bool DeleteProduct(Product product);
        bool UpdateProduct(int id, ProductViewModel model);
        IEnumerable<Product> SearchProduct(string? searchString);
        IEnumerable<Product> FilterProduct(FilterData filter);
    }
}

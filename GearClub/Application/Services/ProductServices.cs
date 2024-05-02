using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Infrastructures.ExtensionMethods;
using GearClub.Presentation.CompositeModels;
using Microsoft.AspNetCore.Mvc;

namespace GearClub.Application.Services
{
    public class ProductServices : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Specification> _specRepo;
        private readonly IRepository<Image> _imageRepo;
        private readonly IRepository<Category_Product> _categoryProductRepo;

        public ProductServices(IRepository<Product> productRepo, IRepository<Specification> specRepo, 
            IRepository<Image> imageRepo, IRepository<Category_Product> categoryProductRepo)
        {
            _productRepo = productRepo;
            _specRepo = specRepo;
            _imageRepo = imageRepo;
            _categoryProductRepo = categoryProductRepo;
        }

        public bool CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> FilterProduct(FilterData filter)
        {
            var filteredProducts = _productRepo.GetAll();           
            //filter by price
            if (filter.PriceRange != null && filter.PriceRange.Count > 0
                && !filter.PriceRange.Contains("all"))
            {
                List<PriceRange> priceRanges = new List<PriceRange>();
                foreach (var priceRange in filter.PriceRange)
                {
                    var value = priceRange.Split('-').ToArray();
                    PriceRange range = new PriceRange();
                    range.Min = Convert.ToInt16(value[0]);
                    range.Max = Convert.ToInt16(value[1]);
                    priceRanges.Add(range);

                }
                filteredProducts = filteredProducts.Where(n => priceRanges
                    .Any(r => n.Price >= r.Min * 1000000 && n.Price <= r.Max * 1000000)).ToList();
            }

            //filter by brand
            var brands = filter.Brand;
            filteredProducts = filteredProducts.Where(p => brands.Any(b => p.Brand.ToLower() == b)).ToList();

            return filteredProducts;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepo.GetAll().ToList(); 
        }

        public Product GetProductById(int productId)
        {
            return _productRepo.GetById(productId);
        }

        public ProductDetailModel GetProductDetail(int productId)
        {
            Product product = _productRepo.GetById(productId);

            var images = from image in _imageRepo.GetAll()
                         where image.ProductId == productId
                         select image;

            List < Specification > specs = _specRepo.GetSpecificationsByProduct(productId);
            ProductDetailModel detailsModel = new ProductDetailModel();
            detailsModel.Product = product;
            detailsModel.Specifications = specs;
            detailsModel.DetailImages = images;
            return detailsModel;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {           
            var products = from product in _productRepo.GetAll()
                       join catePro in _categoryProductRepo.GetAll().Where(c => c.CategoryId == categoryId)
                       on product.ProductId equals catePro.ProductId
                       select product;

            return products;
        }

        public IEnumerable<Product> SearchProduct(string searchString)
        {
            return _productRepo.GetAll().Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }              
    }
}

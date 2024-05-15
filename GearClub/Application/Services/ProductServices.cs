using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Infrastructures.ExtensionMethods;
using GearClub.Presentation.CompositeModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace GearClub.Application.Services
{
    public class ProductServices : IProductService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Specification> _specRepo;
        private readonly IRepository<Domain.Models.Image> _imageRepo;
        private readonly IRepository<Category_Product> _categoryProductRepo;

        public ProductServices(IRepository<Product> productRepo, IRepository<Specification> specRepo,
            IRepository<Domain.Models.Image> imageRepo, IRepository<Category_Product> categoryProductRepo)
        {
            _productRepo = productRepo;
            _specRepo = specRepo;
            _imageRepo = imageRepo;
            _categoryProductRepo = categoryProductRepo;
        }

        public void CreateProduct(ProductViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (_productRepo.GetAll().FirstOrDefault(p => p.Name == model.Product.Name) != null)
            {
                throw new InvalidDataException("Name already taken");
            }

            _productRepo.Add(model.Product);
            var id = _productRepo.GetAll().FirstOrDefault(p => p.Name == model.Product.Name).ProductId;

            //File handler                
            if (model.PreviewImage == null || model.DetailImages == null)
            {
                throw new ArgumentNullException("No images provided");
            }
            try
            {
                var previewImageName = "Preview.webp";
                var previewImagePath = Path.Combine("wwwroot", "img", "ProductImages", "PreviewImages", id.ToString(), previewImageName);

                Directory.CreateDirectory(Path.GetDirectoryName(previewImagePath));

                using (var image = Image.Load(model.PreviewImage.OpenReadStream()))
                {
                    var width = 600;
                    var height = 600;
                    image.Mutate(x => x.Resize(width, height));

                    // Save the image directly to the file
                    image.Save(previewImagePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                }

                foreach (var item in model.DetailImages)
                {
                    var imageName = item.FileName;
                    var imagePath = Path.Combine("wwwroot", "img", "ProductImages", id.ToString(), imageName);

                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    

                    using (var image = Image.Load(item.OpenReadStream()))
                    {
                        var width = 600;
                        var height = 600;
                        image.Mutate(x => x.Resize(width, height));

                        image.Save(imagePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                    }

                    _imageRepo.Add(new Domain.Models.Image
                    {
                        ProductId = id,
                        Link = "/img/ProductImages/" + id.ToString() + "/" + imageName,
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                throw new Exception(ex.Message);
            }
            
        }            
    
        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                product.Deleted_at = DateTime.UtcNow;
                UpdateProduct(product);
                return true;
            }
            catch { return false; }
        }

        public IEnumerable<Product> FilterProduct(FilterData filter)
        {
            var filteredProducts = _productRepo.GetAll();           
            //filter by price
            if(filter.PriceRange.Contains("all"))
            {
                return filteredProducts;
            }
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
            if (filter.Brand.Contains("all"))
            {
                return filteredProducts;
            }

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

        public IEnumerable<Product> SearchProduct(string? searchString)
        {
            if (searchString == null) return _productRepo.GetAll();
            return _productRepo.GetAll().Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                product.Modified_at = DateTime.UtcNow;
                _productRepo.Update(product);
                return true;
            }
            catch { return false; }
        }

        //Update with images
        public bool UpdateProduct(int id, ProductViewModel model)
        {
            Product? product = model.Product;
            if (product == null) return false;
            if (id != product.ProductId)
            {
                return false;
            }

            var previewImagePath = Path.Combine("wwwroot", "img", "ProductImages", "PreviewImages", id.ToString());
            var detailImagesPath = Path.Combine("wwwroot", "img", "ProductImages", id.ToString());

            if (model.PreviewImage != null)
            {
                try
                {
                    //Delete existing files
                    foreach (var image in Directory.GetFiles(previewImagePath))
                    {
                        File.Delete(image);
                    }
                    foreach (var image in Directory.GetFiles(detailImagesPath))
                    {
                        File.Delete(image);
                    }
                    

                    //Add replacements
                    var filePath = Path.Combine(previewImagePath, "Preview.webp");
                    
                    using (var image = Image.Load(model.PreviewImage.OpenReadStream()))
                    {
                        var width = 600;
                        var height = 600;
                        image.Mutate(x => x.Resize(width, height));

                        // Save the image directly to the file
                        image.Save(filePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                    }                    
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if (model.DetailImages != null)
            {
                try
                {
                    foreach (var image in Directory.GetFiles(detailImagesPath))
                    {
                        File.Delete(image);
                    }
                    foreach (var image in model.Product.Images)
                    {
                        _imageRepo.Delete(image);
                    }

                    foreach (var item in model.DetailImages)
                    {
                        var imageName = item.FileName;
                        var imagePath = Path.Combine("wwwroot", "ProductImages", id.ToString(), imageName);

                        using (var image = Image.Load(item.OpenReadStream()))
                        {
                            var width = 600;
                            var height = 600;
                            image.Mutate(x => x.Resize(width, height));

                            image.Save(imagePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                        }
                    }
                    //Update the image links in Image table
                    foreach (var link in Directory.GetFiles(detailImagesPath))
                    {
                        Domain.Models.Image image = new Domain.Models.Image()
                        {
                            Link = link,
                            ProductId = model.Product.ProductId,
                        };
                        _imageRepo.Add(image);
                    }

                }
                catch (Exception)
                {
                    return false;
                }
            }

            try
            {
                UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;

            
        }

        //Utility Methods
        private bool ProductExists(int id)
        {
            return _productRepo.GetAll().Any(e => e.ProductId == id);
        }
    }
}

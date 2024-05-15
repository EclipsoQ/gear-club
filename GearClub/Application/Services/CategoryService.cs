using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;
using GearClub.Presentation.CompositeModels;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace GearClub.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepo;
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<Domain.Models.Image> imageRepo;
        public CategoryService(IRepository<Category> categoryRepo, IRepository<Product> productRepo,
            IRepository<Domain.Models.Image> imageRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
            this.imageRepo = imageRepo;
        }
        public void AddProductToCategory(int productId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public void CreateCategory(CategoryViewModel model)
        {
            var category = model.Category;  
            if (category == null)
            {
                throw new ArgumentNullException();
            }            
            try
            {
                categoryRepo.Add(category);

                var id = categoryRepo.GetAll().FirstOrDefault(c => c.Name == category.Name).CategoryId;                
                if (model.PreviewImage == null)
                {
                    throw new ArgumentNullException();
                }

                var imageName = category.CategoryId.ToString() + ".webp";                
                var imagePath = Path.Combine("wwwroot", "img", "ProductImages", "CategoryImages", imageName);
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                using (var image = Image.Load(model.PreviewImage.OpenReadStream()))
                {
                    var width = 800;
                    var height = 800;
                    image.Mutate(x => x.Resize(width, height));

                    // Save the image directly to the file
                    image.Save(imagePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                }

                imageRepo.Add(new Domain.Models.Image()
                {
                    CategoryId = id,
                    Link = "img/ProductImages/CategoryImages/" + imageName
                });

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                category.IsDeleted = true;
                category.Deleted_at = DateTime.UtcNow;
                categoryRepo.Update(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<Category> GetAllCategories()
        {
            return categoryRepo.GetAll().ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return categoryRepo.GetById(id);
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                category.Modified_at = DateTime.UtcNow;
                categoryRepo.Update(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdateCategory(CategoryViewModel model)
        {
            Category? category = model.Category;
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            
            if (model.PreviewImage != null)
            {
                try
                {
                    var imageName = category.CategoryId.ToString() + ".webp";                        
                    var imagePath = Path.Combine("wwwroot", "img", "ProductImages", "CategoryImages", imageName);

                    File.Delete(imagePath);

                    using (var image = Image.Load(model.PreviewImage.OpenReadStream()))
                    {
                        var width = 800;
                        var height = 800;
                        image.Mutate(x => x.Resize(width, height));

                        // Save the image directly to the file
                        image.Save(imagePath, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder());
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            try
            {
                categoryRepo.Update(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    throw;
                }
            }                                
        }
        private bool CategoryExists(int id)
        {
            return categoryRepo.GetAll().Any(e => e.CategoryId == id);
        }
    }
    
}

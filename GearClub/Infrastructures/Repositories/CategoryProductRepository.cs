using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.Repositories
{
    public class CategoryProductRepository : IRepository<Category_Product>
    {
        private readonly GearClubContext _context;
        public CategoryProductRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Category_Product entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category_Product entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Category_Product> GetAll()
        {
            return _context.Category_Products.ToList();
        }

        public Category_Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category_Product entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }       

        public int CountById(int categoryId)
        {
            return _context.Category_Products.Cast<Category_Product>()
                    .Count(c => c.CategoryId == categoryId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

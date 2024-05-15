using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Infrastructures.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly GearClubContext _context;
        public CategoryRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Image).ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Include(c => c.Category_Products).Include(c => c.Image)
                .FirstOrDefault(c => c.CategoryId == id);                
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
    }
}

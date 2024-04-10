using GearClub.Data;
using GearClub.Models;

namespace GearClub.Repositories
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
             return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
    }
}

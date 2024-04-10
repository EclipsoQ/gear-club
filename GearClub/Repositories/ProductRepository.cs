using GearClub.Data;
using GearClub.Models;

namespace GearClub.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly GearClubContext _context;
        public ProductRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();            
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();  
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
        }
                
    }
}

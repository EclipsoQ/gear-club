using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Infrastructures.Repositories
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

        public Product? GetById(int id)
        {
            return _context.Products.Include(p => p.Specifications)
                                    .Include(p => p.Images)
                                    .Include(p => p.Category_Products)
                                    .ThenInclude(c => c.Category)
                                    .FirstOrDefault(p => p.ProductId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
        }

    }
}

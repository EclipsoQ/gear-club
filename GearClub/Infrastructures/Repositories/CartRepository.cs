using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly GearClubContext _context;
        public CartRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Cart entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(string userId)
        {
            throw new NotImplementedException();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Cart entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public Cart GetById(int id)
        {
            return _context.Carts.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Cart entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

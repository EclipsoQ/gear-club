using GearClub.Data;
using GearClub.Models;

namespace GearClub.Repositories
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

        public int CountById(int id)
        {
            return _context.Carts.Count();
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

        public void Update(Cart entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

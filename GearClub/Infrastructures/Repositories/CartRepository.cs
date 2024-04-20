using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.Repositories
{
    public class CartDetailRepository : IRepository<CartDetail>
    {
        private readonly GearClubContext _context;
        public CartDetailRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(CartDetail entity)
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

        public void Delete(CartDetail entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<CartDetail> GetAll()
        {
            return _context.CartDetails.ToList();
        }

        public CartDetail GetById(int id)
        {
            return _context.CartDetails.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(CartDetail entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

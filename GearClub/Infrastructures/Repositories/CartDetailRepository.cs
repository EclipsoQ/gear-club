using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public int CountById(int id)
        {
            return _context.CartDetails.Count();
        }

        public void Delete(CartDetail entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<CartDetail> GetAll()
        {
            return _context.CartDetails.Include(cd => cd.Product).ToList();
        }

        public CartDetail GetById(int id)
        {
            return _context.CartDetails.Include(cd => cd.Product)
                .FirstOrDefault(cd => cd.CartDetailId == id);
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

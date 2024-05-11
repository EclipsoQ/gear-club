using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Infrastructures.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly GearClubContext _context;
        public OrderRepository(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Order entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Address).ThenInclude(a => a.User).ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.Include(o => o.Address)
                .ThenInclude(a => a.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

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
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.Find(id);
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

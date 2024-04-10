using GearClub.Data;
using GearClub.Models;

namespace GearClub.Repositories
{
    public class OrderDetailRepo : IRepository<Order>
    {
        private readonly GearClubContext _context;
        public OrderDetailRepo(GearClubContext context)
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
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

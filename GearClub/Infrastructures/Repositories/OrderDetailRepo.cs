using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.Repositories
{
    public class OrderDetailRepo : IRepository<OrderDetail>
    {
        private readonly GearClubContext _context;
        public OrderDetailRepo(GearClubContext context)
        {
            _context = context;
        }

        public void Add(OrderDetail entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderDetail entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(OrderDetail entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

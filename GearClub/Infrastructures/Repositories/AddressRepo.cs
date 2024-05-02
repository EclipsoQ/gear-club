using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using Microsoft.EntityFrameworkCore;

namespace GearClub.Infrastructures.Repositories
{    
    public class AddressRepo : IRepository<Address>
    {
        private readonly GearClubContext _context;
        public AddressRepo(GearClubContext context) 
        {
            _context = context;
        }
        public void Add(Address entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Address entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Address> GetAll()
        {
            return _context.Addresses.Include(a => a.User).Include(a => a.Orders).ToList();
        }

        public Address GetById(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Address entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

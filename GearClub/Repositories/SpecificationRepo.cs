using GearClub.Data;
using GearClub.Models;

namespace GearClub.Repositories
{
    public class SpecificationRepo : IRepository<Specification>
    {
        private readonly GearClubContext _context;
        public SpecificationRepo(GearClubContext context)
        {
            _context = context;
        }

        public void Add(Specification entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Specification entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Specification> GetAll()
        {
            return _context.Specifications.ToList();
        }

        public Specification GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Specification entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}

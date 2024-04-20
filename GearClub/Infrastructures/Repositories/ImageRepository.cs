using GearClub.Data;
using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.Repositories
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly GearClubContext _context;
        public ImageRepository(GearClubContext context)
        {
            _context = context;
        }
        public void Add(Image entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public int CountById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Image entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Images.ToList();
        }

        public Image GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Image entity)
        {
            throw new NotImplementedException();
        }
    }
}

using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;

namespace GearClub.Infrastructures.ExtensionMethods
{
    public static class SpecRepoExtension
    {
        public static List<Specification> GetSpecificationsByProduct(this IRepository<Specification> repository, int productId)
        {
            return repository.GetAll().Where(s => s.ProductId == productId).ToList(); 
        }
    }
}

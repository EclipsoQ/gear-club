using GearClub.Domain.Models;
using GearClub.Domain.RepoInterfaces;
using GearClub.Domain.ServiceInterfaces;

namespace GearClub.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> addressRepo;
        public AddressService(IRepository<Address> addressRepo)
        {
            this.addressRepo = addressRepo;
        }
        public bool CreateAddress(Address address)
        {
            try
            {
                addressRepo.Add(address);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAddress(Address address)
        {
            try
            {
                addressRepo.Delete(address);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Address GetAddressById(int id)
        {
            return addressRepo.GetById(id);
        }

        public List<Address> GetAddressesByUser(string userId)
        {
            return addressRepo.GetAll().Where(a => a.UserId == userId).ToList(); 
        }

        public bool UpdateAddress(Address address)
        {
            try
            {
                addressRepo.Update(address);
                return true;
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}

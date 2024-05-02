using GearClub.Domain.Models;

namespace GearClub.Domain.ServiceInterfaces
{
    public interface IAddressService
    {
        List<Address> GetAddressesByUser(string userId);
        bool CreateAddress(Address address);
        bool DeleteAddress(Address address);
        bool UpdateAddress(Address address);
        Address GetAddressById(int id);
    }
}

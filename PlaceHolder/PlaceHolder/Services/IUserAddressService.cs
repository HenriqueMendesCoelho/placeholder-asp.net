using PlaceHolder.DTOs;
using PlaceHolder.Integrations.ViaCEP.Model;

namespace PlaceHolder.Services
{
    public interface IUserAddressService
    {
        UserAddress Create(UserAddress userAddress);

        UserAddress? Update(UserAddress userAddress);

        void Delete(long id);

        UserAddress? FindByID(long id);

        List<UserAddress> FindAll();

        public UserAddress ExtractAddresFromDTO(AddressDTO obj, long id, ViaCEPResponse viaCEP);
    }
}

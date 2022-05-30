using PlaceHolder.DTOs;
using PlaceHolder.Integrations.ViaCEP.Model;

namespace PlaceHolder.Services.Implamentations
{
    public class UserAddressImplementation : IUserAddressService
    {

        private readonly IRepository<UserAddress> _repository;

        public UserAddressImplementation(IRepository<UserAddress> repository)
        {
            _repository = repository;
        }

        public UserAddress Create(UserAddress userAddress)
        {

            _repository.Create(userAddress);

            return userAddress;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<UserAddress> FindAll()
        {
            return _repository.FindAll();
        }

        public UserAddress? FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public UserAddress? Update(UserAddress userAddress)
        {
            return _repository.Update(userAddress);
        }

        public UserAddress ExtractAddresFromDTO(AddressDTO obj, long id, ViaCEPResponse viaCEP)
        {
            return new UserAddress()
            {
                Cep = viaCEP.Cep.Replace("-", ""),
                State = (!string.IsNullOrEmpty(viaCEP.Uf)) ? viaCEP.Uf : obj.State,
                City = (!string.IsNullOrEmpty(viaCEP.Localidade)) ? viaCEP.Localidade : obj.City,
                District = (!string.IsNullOrEmpty(viaCEP.Bairro)) ? viaCEP.Bairro : obj.District,
                Street = (!string.IsNullOrEmpty(viaCEP.Logradouro)) ? viaCEP.Logradouro : obj.Street,
                Complement = obj.Complement,
                Id = id,
                Number = obj.Number
            };
        }
    }
}

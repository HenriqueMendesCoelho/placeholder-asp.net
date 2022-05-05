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
    }
}

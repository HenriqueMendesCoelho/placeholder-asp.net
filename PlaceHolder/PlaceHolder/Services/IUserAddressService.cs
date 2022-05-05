namespace PlaceHolder.Services
{
    public interface IUserAddressService
    {
        UserAddress Create(UserAddress userAddress);

        UserAddress? Update(UserAddress userAddress);

        void Delete(long id);

        UserAddress? FindByID(long id);

        List<UserAddress> FindAll();
    }
}

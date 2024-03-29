﻿using PlaceHolder.DTOs;

namespace PlaceHolder.Services.Implamentations
{
    public interface IUserService
    {
        User Create(UserDTO user);

        User? Update(User user);

        void Delete(string email);

        User? FindByID(long id);

        User? FindByEmail(string email);

        List<User> FindAll();

        bool ValidateCredencials(string Email, string Password);

        string EncryptPassword(string input);
    }
}

﻿using PlaceHolder.Validation_Attributes;

namespace PlaceHolder.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "FullName must have maximum of {1} characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "CPF is required")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "CPF format not valid")]
        [CpfValidation(ErrorMessage ="CPF not valid")]
        [CPFUnique(ErrorMessage = "E-mail already used")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must have minimum of {1} characters")]
        [MaxLength(40, ErrorMessage = "Password must have maximum of {1} characters")]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%¨&*()_+-=´`{}^~:;?|'<,>.]).{8,40}", 
        ErrorMessage = "Password must contain uppercase, lowercase, special character, number and at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [EmailUnique(ErrorMessage ="E-mail already used")]
        public string Email { get; set; }
        public UserDTO()
        {

        }
        public UserDTO(string fullName, string cpf, string password, string email)
        {
            FullName = fullName;
            Cpf = cpf;
            Password = password;
            Email = email;
        }
    }
}

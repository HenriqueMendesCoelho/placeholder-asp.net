using PlaceHolder.Validation_Attributes;

namespace PlaceHolder.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "FullName must have maximum of 50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "CPF is required")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "CPF format not valid")]
        [CpfValidation(ErrorMessage ="CPF not valid")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must have minimum of 8 characters")]
        [MaxLength(20, ErrorMessage = "Password must have maximum of 20 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailUnique(ErrorMessage ="E-mail already used")]
        public string Email { get; set; }

        [Required(ErrorMessage = "BackupEmail is required")]
        public string BackupEmail { get; set; }
        public UserDTO()
        {

        }
        public UserDTO(string fullName, string cpf, string password, string email, string backupEmail)
        {
            FullName = fullName;
            Cpf = cpf;
            Password = password;
            Email = email;
            BackupEmail = backupEmail;
        }
    }
}

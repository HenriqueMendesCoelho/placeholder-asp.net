namespace PlaceHolder.DTOs
{
    public class ChangePasswordUserDTO
    {
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must have minimum of {1} characters")]
        [MaxLength(40, ErrorMessage = "Password must have maximum of {1} characters")]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%¨&*()_+-=´`{[]}^~:;?/'<,>.]).{8,40}",
            ErrorMessage = "Password must contain uppercase, lowercase, special character, number and at least 8 characters")]
        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}

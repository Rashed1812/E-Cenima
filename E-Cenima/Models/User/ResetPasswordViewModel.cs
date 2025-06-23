using System.ComponentModel.DataAnnotations;

namespace E_Cenima.Models.User
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

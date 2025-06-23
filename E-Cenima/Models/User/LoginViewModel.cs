using System.ComponentModel.DataAnnotations;

namespace E_Cenima.Models.User
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

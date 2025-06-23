using System.ComponentModel.DataAnnotations;

namespace E_Cenima.Models.User
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}

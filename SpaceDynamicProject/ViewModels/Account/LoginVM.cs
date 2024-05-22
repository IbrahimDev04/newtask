using System.ComponentModel.DataAnnotations;

namespace SpaceDynamicProject.ViewModels.Account
{
    public class LoginVM
    {
        [MaxLength(16, ErrorMessage = "Lengt Error"), MinLength(6, ErrorMessage = "Lengt Error"), Required]
        public string UsernameOrEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

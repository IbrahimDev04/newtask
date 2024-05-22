using System.ComponentModel.DataAnnotations;

namespace SpaceDynamicProject.ViewModels.Account
{
    public class RegisterVM
    {
        [MaxLength(16, ErrorMessage = "Lengt Error"), MinLength(6, ErrorMessage = "Lengt Error"), Required]
        public string Name { get; set; }

        [MaxLength(16, ErrorMessage = "Lengt Error"), MinLength(6, ErrorMessage = "Lengt Error"), Required]
        public string Surname { get; set; }

        [MaxLength(16, ErrorMessage = "Lengt Error"), MinLength(6, ErrorMessage = "Lengt Error"), Required]
        public string Usename { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string RepidePassword { get; set; }
    }
}

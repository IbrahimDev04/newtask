using System.ComponentModel.DataAnnotations;

namespace SpaceDynamicProject.ViewModels.Card
{
    public class CreateCardVM
    {
        [Required, MaxLength(32, ErrorMessage = "Lenght Error")]
        public string Title { get; set; }

        [Required, MaxLength(32, ErrorMessage = "Lenght Error")]
        public string Description { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}

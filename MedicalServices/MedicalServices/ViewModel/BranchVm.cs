using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class BranchVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string ContanctPhone { get; set; }
        [Required]
        public IFormFile Image { get; set; }


    }
}

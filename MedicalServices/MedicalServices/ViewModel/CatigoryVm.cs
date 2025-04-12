using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class CatigoryVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class RoleVm
    {
        [Required]
        public string RoleName { get; set; }

    }
}

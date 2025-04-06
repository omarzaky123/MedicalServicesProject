using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class LoginGusetVm
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

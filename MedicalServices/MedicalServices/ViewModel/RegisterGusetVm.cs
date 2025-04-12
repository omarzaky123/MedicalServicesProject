using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class RegisterGusetVm
    {
        [MinLength(1,ErrorMessage ="The name must be greater than 1")]
        [Required(ErrorMessage = "The field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime AppoinmentDate { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        [MinLength(1, ErrorMessage = "The address must be greater than 1")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The field is Required")]
        public DateTime Age { get; set; }
    }
}

using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace MedicalServices.Models
{
    public class ApplicationUser:IdentityUser,IHasImage
    {
        public string Address { get; set; }
        public int Age { get; set; }
        public string? Image { get; set; }
        public decimal? Salary { get; set; } 
    }
}

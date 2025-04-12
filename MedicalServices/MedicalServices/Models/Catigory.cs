using MedicalServices.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace MedicalServices.Models
{
    public class Catigory: InterfaceForSort,InterfaceByID
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public virtual List<MedicalService> MedicalServices { get; set; }=new List<MedicalService>();
        
    }
}

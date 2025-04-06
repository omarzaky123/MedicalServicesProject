using MedicalServices.Repository;
using MedicalServices.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class Doctor:IWorker, InterfaceByID
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [ForeignKey("Branch")]
        public int? BranchID { get; set; }
        public virtual Branch Branch { get; set; }  
        public virtual ApplicationUser User { get; set; }

        

    }
}

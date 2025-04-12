using MedicalServices.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class BranchGusetService: InterfaceByID, InterfaceByDate, InterfaceForBGS, InterfaceForSortBranchId
    {
        public int ID { get; set; }
        [ForeignKey("Branch")]
        public int? BranchID { get; set; }

        [ForeignKey("Guset")]
        public int? GusetID { get; set; }

        [ForeignKey("MedicalService")]
        public int? ServiceID { get; set; }
		[ForeignKey("DateBranch")]
        public int? DateBranchID { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Guset Guset { get; set; }
        public virtual MedicalService MedicalService { get; set; }
        public virtual DateBranch DateBranch { get; set; }

        public bool? EmailSent { get; set; }
        public bool? Uploaded { get; set; }


    }
}

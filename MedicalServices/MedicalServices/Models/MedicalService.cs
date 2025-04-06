using MedicalServices.Repository;
using MedicalServices.ViewModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class MedicalService: InterfaceForSort,InterfaceByID, InterfaceForSortBranchId, IHasImage
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Branch")]
        public int? BranchID { get; set; }
        [ForeignKey("Catigory")]
        public int? CatigoryID { get; set; }
        public virtual Branch  Branch { get; set; }
        public virtual Catigory Catigory { get; set; }
        public virtual List<BranchGusetService> BranchGusetServices { get; set; } = new List<BranchGusetService>();

    }
}

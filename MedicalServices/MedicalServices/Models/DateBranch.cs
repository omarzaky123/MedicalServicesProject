using MedicalServices.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class DateBranch
    {
        public int ID { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        [ForeignKey("Date")]
        public int DateId { get; set; }

        public bool State { get; set; }

        public virtual Date Date { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual List<BranchGusetService> BranchGusetServices { get; set; } = new List<BranchGusetService>();

    }
}

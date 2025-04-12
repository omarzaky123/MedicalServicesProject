using MedicalServices.Models;

namespace MedicalServices.ViewModel
{
    public interface IWorker:IBaseInterface
    {
        int ID { get; set; }
        string UserID { get; set; }
        int? BranchID { get; set; }
        Branch Branch { get; set; }
        ApplicationUser User { get; set; }
    }
}

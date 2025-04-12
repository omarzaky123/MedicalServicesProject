using MedicalServices.Models;
using MedicalServices.ViewModel;

namespace MedicalServices.Repository
{
    public interface IDateRepository
    {
        List<Date> UpdateDates();
        List<Date> GetDates();
        void DeleteAllDateForBranch();
        void AddDateForBranch(int branchid, int dateid);

        void DeleteDateForBranch(int branchid);

        string DateName(int dateid);
    }
}

using MedicalServices.Models;
using MedicalServices.ViewModel;

namespace MedicalServices.Repository
{
    public interface ISortAndSearchRepository<T>
    {
        List<T> SortByName(int branchid = 0);
        List<T> SortByNameDescending(int branchid = 0);
        List<T> StateSort(string Sort, int branchid = 0);
        List<T> SortWorkerByName();
        List<T> SortWorkerByNameDes();
        List<T> SortWorkerBySalary();
        List<T> SortWorkerByAge();
        List<T> SortByDate(int branchid = 0);
        List<T> SearchByName(string name, int branchid=0);
        List<T> SearchByGuset(string name, int branchid = 0);
        List<T> SearchWorkerByName(string name);

        List<T> SortById(int branchid = 0);
        List<T> SortByIdDescending(int branchid = 0);

    }
    
}

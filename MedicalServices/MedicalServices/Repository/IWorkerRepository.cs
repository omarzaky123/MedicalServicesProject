using MedicalServices.Models;
using MedicalServices.ViewModel;

namespace MedicalServices.Repository
{
    public interface IWorkerRepository<T>
         where T : class, IWorker  // Added 'class' constraint
    {
        T GetByUserId02(string userId);
    }
}

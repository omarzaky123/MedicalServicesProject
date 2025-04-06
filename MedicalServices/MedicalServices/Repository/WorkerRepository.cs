using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class WorkerRepository<T> : IWorkerRepository<T>
        where T : class, IWorker  // Added 'class' constraint
    {
        private readonly MyContext myContext;
        private readonly DbSet<T> dbSet;

        public WorkerRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = this.myContext.Set<T>();
        }

        public T GetByUserId02(string userId)
        {
            T worker = dbSet.FirstOrDefault(X=>X.UserID==userId);
            return worker;
        }
    }
}
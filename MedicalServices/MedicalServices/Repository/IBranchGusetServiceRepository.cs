using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IBranchGusetServiceRepository
    {
        void Insert(BranchGusetService BGS);
        void Delete(int branchid, int gusetid);
        void SetEamilSent(int id, bool State = true);

        List<BranchGusetService> GetForCertainBranchNotUplodedOrders(int branchid = 0);

        void SetUploade(int id, bool State = true);
    }
}

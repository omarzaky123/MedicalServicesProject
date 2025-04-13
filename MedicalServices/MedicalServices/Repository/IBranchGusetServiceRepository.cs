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

        List<MedicalService> RelatedOrdersForCertainBranch(int guestId, int branchid = 0);
        int GetIdByServicesIdAndGusetNameId(int gusetId, int serviceId);
        List<Guset> GetGusetsForCertainBranch(int branchid = 0);
    }
}

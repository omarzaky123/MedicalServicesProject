using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface ICatigoryRepository
    {
        List<MedicalService> GetRelatedServicesBranch(int BranchId, int CatigoryId);
    }
}

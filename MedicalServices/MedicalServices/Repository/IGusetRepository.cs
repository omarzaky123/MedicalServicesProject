//using MedicalServices.Migrations;
using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IGusetRepository
    {
        Guset AccountId(string id);
        List<BranchGusetService> RelatedBGS(int id);

    }
}

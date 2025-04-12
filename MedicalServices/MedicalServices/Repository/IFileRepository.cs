using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IFileRepository
    {
        List<UplodedFile> GetAll();
        void InsertRange(List<UplodedFile> uplodedFiles);
        UplodedFile GetByStoredName(string storedName);
        void Delete(string storedName);
        List<UplodedFile> GetAllForCertainBranch(int branchId);
    }
}

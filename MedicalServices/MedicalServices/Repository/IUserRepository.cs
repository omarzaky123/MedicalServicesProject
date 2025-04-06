using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IUserRepository
    {
        void Delete(string userid);
        ApplicationUser GetById(string id);
    }
}

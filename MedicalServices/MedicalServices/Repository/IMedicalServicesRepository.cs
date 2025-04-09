using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IMedicalServicesRepository
    {
        public void Update(int id, MedicalService NewMedical);
    }
}

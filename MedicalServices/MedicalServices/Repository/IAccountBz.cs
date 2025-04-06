using MedicalServices.Models;
using MedicalServices.ViewModel;

namespace MedicalServices.Repository
{
    public interface IAccountBz
    {
        public void SetInTheTable(string RoleName, ApplicationUser applcationUser, AssignRoleVm assignRoleVm);
    }
}
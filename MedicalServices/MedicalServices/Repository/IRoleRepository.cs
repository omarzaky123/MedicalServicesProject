using Microsoft.AspNetCore.Identity;

namespace MedicalServices.Repository
{
    public interface IRoleRepository
    {
        List<IdentityRole> GetRolesForAdmin();
    }
}

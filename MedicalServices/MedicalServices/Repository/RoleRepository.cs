using MedicalServices.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace MedicalServices.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<IdentityRole> GetRolesForAdmin()
        {
            List<IdentityRole> roles = _roleManager.Roles.Where(R => R.Name != "SuperAdmin" && R.Name != "Admin").ToList();    
            return roles;
        }
    }
}
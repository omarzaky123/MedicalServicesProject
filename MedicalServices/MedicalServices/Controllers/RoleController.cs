using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IGeneralRepository<IdentityRole> generalRoleRepository;

        #region Ctor
        public RoleController(RoleManager<IdentityRole> _roleManager,
            IGeneralRepository<IdentityRole> _generalRoleRepository)
        {
            roleManager = _roleManager;
            generalRoleRepository = _generalRoleRepository;
        } 
        #endregion

        #region Index
        public IActionResult Index()
        {
            List<IdentityRole> roles = new List<IdentityRole>();
            return View(generalRoleRepository.GetAll());
        }


        #endregion


        #region New
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(RoleVm roleVm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleVm.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(roleVm);
        } 
        #endregion

    }
}



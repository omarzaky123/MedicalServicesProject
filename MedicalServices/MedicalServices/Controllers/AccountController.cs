using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    public class AccountController : Controller
    {
        #region Ctor
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IGeneralRepository<Guset> generalGusetRepository;
        private readonly IGusetRepository GusetRepository;
        private readonly IGeneralRepository<IdentityRole> generalRoleRepository;
        private readonly IGeneralRepository<Branch> generalBranchRepository;
        private readonly IAccountBz accountBz;
        private readonly IWorkerRepository<Admin> workerRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IImageRepository imageRepository;
        private readonly IGeneralRepository<BranchGusetService> generalBranchGusetRepository; 
        #endregion

        #region Ctor
        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            IGeneralRepository<Guset> _generalGusetRepository,
            IGeneralRepository<BranchGusetService> _generalBranchGusetRepository,
           IGusetRepository _gusetRepository ,
           IGeneralRepository<IdentityRole> _generalRoleRepository,
           IGeneralRepository<Branch> _generalBranchRepository,
           IAccountBz accountBz,
           IWorkerRepository<Admin> workerRepository,
           IRoleRepository roleRepository,
           IImageRepository imageRepository
           )
        {
            userManager = _userManager;
            signInManager = _signInManager;
            generalGusetRepository = _generalGusetRepository;
            generalBranchGusetRepository = _generalBranchGusetRepository;
            this.GusetRepository = _gusetRepository;
            generalRoleRepository = _generalRoleRepository;
            generalBranchRepository = _generalBranchRepository;
            this.accountBz = accountBz;
            this.workerRepository = workerRepository;
            this.roleRepository = roleRepository;
            this.imageRepository = imageRepository;
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            //TempData["ReturnToPage"] =ReturnToPage;
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterGusetVm registerGusetVm)
        {
            if (ModelState.IsValid) {
                ApplicationUser applicationUser = new ApplicationUser();
                Guset guset = new Guset();
                applicationUser.Address = registerGusetVm.Address;
                applicationUser.UserName= registerGusetVm.Name;
                applicationUser.Age = DateTime.Now.Year - registerGusetVm.Age.Year;
                applicationUser.PasswordHash= registerGusetVm.Password;
                applicationUser.Email = registerGusetVm.Email;
                IdentityResult result = await userManager.CreateAsync(applicationUser,registerGusetVm.Password);

                if (result.Succeeded)
                {
                    //CreateCokkie
                    await userManager.AddToRoleAsync(applicationUser, "Guset");
                    await signInManager.SignInAsync(applicationUser,false);
                    guset.UserID = applicationUser.Id;
                    generalGusetRepository.Insert(guset);
                    CookieOptions cookieOptions = new CookieOptions();
                    Response.Cookies.Append("GusetId", guset.ID.ToString(), cookieOptions);
                    if ((bool)TempData["ReturnToPage"]==true)
                        return RedirectToAction("WelcomePage", "Welcome");
                    else
                        return RedirectToAction("Invoice","Welcome");
                }
                else {
                    foreach (var item in result.Errors) ModelState.AddModelError("",item.Description);
                    
                }


            }
            return View(registerGusetVm);
        }
        #endregion

        #region AssignRole
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public IActionResult AssignRole()
        {    
            if (User.IsInRole("Admin"))
            {
                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Admin admin = workerRepository.GetByUserId02(userid);
                List<Branch> branch = new List<Branch>();
                branch.Add(admin.Branch);
                ViewBag.Branches=branch;
                ViewBag.Roles = roleRepository.GetRolesForAdmin();
            }
            else
            {
                ViewBag.Branches = generalBranchRepository.GetAll();
                ViewBag.Roles = generalRoleRepository.GetAll();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AssignRole(AssignRoleVm assignRoleVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = generalRoleRepository.GetAll();
                ViewBag.Branches = generalBranchRepository.GetAll();
                return View(assignRoleVm);
            }

            ApplicationUser applicationUser = new ApplicationUser();
            await imageRepository.SaveImageInWroot(applicationUser, assignRoleVm.Image);
            applicationUser.UserName = assignRoleVm.Name;
            applicationUser.Address = assignRoleVm.Address;
            applicationUser.Email = assignRoleVm.Email;
            applicationUser.PasswordHash = assignRoleVm.Password;
            applicationUser.Age = DateTime.Now.Year - assignRoleVm.Age.Year;
            applicationUser.Salary = assignRoleVm.Salary;

            IdentityResult result = await userManager.CreateAsync(applicationUser, assignRoleVm.Password);

            if (!result.Succeeded)
            {
                ViewBag.Roles = generalRoleRepository.GetAll();
                ViewBag.Branches = generalBranchRepository.GetAll();
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(assignRoleVm);
            }

            // Assign to Role
            string RoleName = assignRoleVm.RoleName;
            await userManager.AddToRoleAsync(applicationUser, RoleName);

            // Create related table entries
            accountBz.SetInTheTable(RoleName, applicationUser, assignRoleVm);

            return RedirectToAction("GetAllWorkers", "Workers");
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(bool ReturnToPage = false)
        {
            TempData["ReturnToPage"]=ReturnToPage;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginGusetVm loginGusetVm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginGusetVm.UserName);
                if (user != null) {
                    bool found = await userManager.CheckPasswordAsync(user,loginGusetVm.Password);
                    if (found) {
                        await signInManager.SignInAsync(user, loginGusetVm.RememberMe);
                        string id = user.Id;
                        Guset guset = GusetRepository.AccountId(id);
                        List<IdentityRole> roles = generalRoleRepository.GetAll();
                        foreach (IdentityRole role in roles) {
                            if (User.IsInRole("Guset"))
                            {
                                if (guset != null)
                                {
                                    CookieOptions cookieOptions = new CookieOptions();
                                    Response.Cookies.Append("GusetId", guset.ID.ToString(), cookieOptions);
                                }

                                if ((bool)TempData["ReturnToPage"] == true)
                                {
                                    return RedirectToAction("WelcomePage", "Welcome");
                                }

                                else
                                {
                                    return RedirectToAction("Invoice", "Welcome");
                                }
                            }
                            else
                            {
                                return RedirectToAction("AdminHome", "AdminWelcome");
                            }
                        }    
                    }
                }
                ModelState.AddModelError("","The UserName or the password is invalid");
            }
            return View(loginGusetVm);
        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("WelcomePage", "Welcome");

        }

        #endregion

    }
}




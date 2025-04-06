using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    public class WorkersController : Controller
    {
        #region CTor
        private readonly IGeneralRepository<Doctor> generaldoctorRepository;
        private readonly IGeneralRepository<Assastant> generalassastantRepository;
        private readonly IGeneralRepository<Accountant> generalaccountRepository;
        private readonly IGeneralRepository<Admin> generaladminRepository;
        private readonly IUserRepository usersRepository;
        private readonly ISortAndSearchRepository<Doctor> sortAndSearchRepository;
        private readonly ISortAndSearchRepository<Assastant> sortAndSearchRepositoryAss;
        private readonly ISortAndSearchRepository<Accountant> sortAndSearchRepositoryAcc;
        private readonly ISortAndSearchRepository<Admin> sortAndSearchRepositoryAdmin;
        private readonly IBranchRepository branchRepository;
        private readonly IWorkerRepository<Admin> workerRepository;
        private readonly IAccountBz accountBz;
        private readonly IImageRepository imageRepository;

        public WorkersController(IGeneralRepository<Doctor> _generaldoctorRepository,
            IGeneralRepository<Assastant> _generalassastantRepository,
            IGeneralRepository<Accountant> generalaccountRepository,
            IGeneralRepository<Admin> _generaladminRepository,
            IUserRepository _usersRepository,
            ISortAndSearchRepository<Doctor> sortAndSearchRepository,
            ISortAndSearchRepository<Assastant> sortAndSearchRepositoryAss,
            ISortAndSearchRepository<Accountant> sortAndSearchRepositoryAcc,
            ISortAndSearchRepository<Admin> sortAndSearchRepositoryAdmin,
            IBranchRepository branchRepository,
            IWorkerRepository<Admin> workerRepository,
            IAccountBz accountBz,
            IImageRepository imageRepository
            )
        {
            generaldoctorRepository = _generaldoctorRepository;
            generalassastantRepository = _generalassastantRepository;
            this.generalaccountRepository = generalaccountRepository;
            generaladminRepository = _generaladminRepository;
            usersRepository = _usersRepository;
            this.sortAndSearchRepository = sortAndSearchRepository;
            this.sortAndSearchRepositoryAss = sortAndSearchRepositoryAss;
            this.sortAndSearchRepositoryAcc = sortAndSearchRepositoryAcc;
            this.sortAndSearchRepositoryAdmin = sortAndSearchRepositoryAdmin;
            this.branchRepository = branchRepository;
            this.workerRepository = workerRepository;
            this.accountBz = accountBz;
            this.imageRepository = imageRepository;
        }
        #endregion

        #region GetAll

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult GetAllWorkers(int CurrentPage=1,string State="")
        {
            List<Doctor> doctors=new List<Doctor>();
            List<Assastant> assastants=new List<Assastant>();
            List<Accountant> accountants=new List<Accountant>();
            List<Admin> admins=new List<Admin>();

            if (User.IsInRole("SuperAdmin"))
            {
                doctors = generaldoctorRepository.GetAll();
                assastants = generalassastantRepository.GetAll();
                accountants = generalaccountRepository.GetAll();
                 admins = generaladminRepository.GetAll();
            }
            else if (User.IsInRole("Admin")){
                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Admin admin = workerRepository.GetByUserId02(userid);
                doctors = branchRepository.GetRelatedDoctors(admin?.BranchID??0);
                assastants =branchRepository.GetRelatedAssastant(admin?.BranchID ?? 0);
                accountants = branchRepository.GetRelatedAccounts(admin?.BranchID ?? 0);
                admins = branchRepository.GetRelatedAdmins(admin?.BranchID ?? 0);
            }

            List<IWorker> mergedList = doctors.Cast<IWorker>()
                                 .Concat(assastants.Cast<IWorker>())
                                 .Concat(accountants.Cast<IWorker>())
                                 .Concat(admins.Cast<IWorker>())
                                 .ToList();

            PaginationVm<IWorker> paginationVm = new PaginationVm<IWorker>(5, CurrentPage, mergedList);
            ViewBag.Pagination = paginationVm;
            mergedList =paginationVm.Items;
            
            return View(mergedList);
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllDoctors(int CurrentPage=1,string Sort="")
        {
            List<Doctor> doctors = sortAndSearchRepository.StateSort(Sort);
            PaginationVm<Doctor> paginationVm = new PaginationVm<Doctor>(2,CurrentPage,doctors);
            ViewBag.Pagination=paginationVm;
            doctors=paginationVm.Items;
            ViewBag.Action = "GetAllDoctors";
            ViewBag.Detail = "DoctorDetails";
            ViewBag.Name = "All Doctors";
            ViewBag.Sort=Sort;
            ViewBag.Delete = "DeleteDoctor";
            return View("GetAllWorkerCertainPosition",doctors.Cast<IWorker>().ToList());
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllAssastants(int CurrentPage=1, string Sort = "")
        {
            List<Assastant> assastants = sortAndSearchRepositoryAss.StateSort(Sort);
            PaginationVm<Assastant> paginationVm = new PaginationVm<Assastant>(2, CurrentPage, assastants);
            ViewBag.Pagination = paginationVm;
            assastants = paginationVm.Items;
            ViewBag.Action = "GetAllAssastants";
            ViewBag.Detail = "AssastantDetails";
            ViewBag.Name = "All Assastant";
            ViewBag.Delete="DeleteAssastant";
            ViewBag.Sort = Sort;
            return View("GetAllWorkerCertainPosition", assastants.Cast<IWorker>().ToList());
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllAccountants(int CurrentPage = 1, string Sort = "")
        {
            List<Accountant> accountants = sortAndSearchRepositoryAcc.StateSort(Sort);
            PaginationVm<Accountant> paginationVm = new PaginationVm<Accountant>(2, CurrentPage, accountants);
            ViewBag.Pagination = paginationVm;
            accountants = paginationVm.Items;
            ViewBag.Action = "GetAllAccountants";
            ViewBag.Detail = "AccountantDetails";
            ViewBag.Name = "All Accountants";
            ViewBag.Sort = Sort;
            ViewBag.Delete = "DeleteAccountant";
            return View("GetAllWorkerCertainPosition", accountants.Cast<IWorker>().ToList());
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllAdmins(int CurrentPage = 1, string Sort = "")
        {

            List<Admin> admins = sortAndSearchRepositoryAdmin.StateSort(Sort);
            PaginationVm<Admin> paginationVm = new PaginationVm<Admin>(2, CurrentPage, admins);
            ViewBag.Pagination = paginationVm;
            admins = paginationVm.Items;
            ViewBag.Action = "GetAllAdmins";
            ViewBag.Detail = "AdminDetails";
            ViewBag.Name = "All Admins";
            ViewBag.Sort = Sort;
            ViewBag.Delete = "DeleteAdmin";
            return View("GetAllWorkerCertainPosition", admins.Cast<IWorker>().ToList());
        }
        #endregion

        #region Details

        public IActionResult DoctorDetails(int id)
        {
            return View("WorkerDetails", generaldoctorRepository.GetById(id));
        }
        public IActionResult AdminDetails(int id)
        {
            return View("WorkerDetails", generaladminRepository.GetById(id));
        }
        public IActionResult AssastantDetails(int id)
        {
            return View("WorkerDetails", generalassastantRepository.GetById(id));
        }
        public IActionResult AccountantDetails(int id)
        {
            return View("WorkerDetails", generalaccountRepository.GetById(id));
        }

        #endregion

        #region Edit

        public IActionResult UpdateById(string id)
        {
            return PartialView(usersRepository.GetById(id));
        }

        #endregion

        #region Delete
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteDoctor(string id, bool ReturnToBranch = false, int BranchId = 0, bool RetuenToWorkers = false)
        {

            ApplicationUser applicationUser=usersRepository.GetById(id);
            if (applicationUser != null)
            {
                imageRepository.DeleteImageInWroot(applicationUser.Image);
            }
            usersRepository.Delete(id);
            if (ReturnToBranch)
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            else if (RetuenToWorkers)
                return RedirectToAction("GetAllWorkers");
            return RedirectToAction("GetAllDoctors");
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteAssastant(string id, bool ReturnToBranch = false, int BranchId = 0, bool RetuenToWorkers = false)
        {
            ApplicationUser applicationUser = usersRepository.GetById(id);
            if (applicationUser != null)
            {
                imageRepository.DeleteImageInWroot(applicationUser.Image);
            }
            usersRepository.Delete(id);
            if (ReturnToBranch)
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            else if (RetuenToWorkers)
                return RedirectToAction("GetAllWorkers");
            return RedirectToAction("GetAllAssastants");
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteAccoutant(string id, bool ReturnToBranch = false, int BranchId = 0, bool RetuenToWorkers = false)
        {
            ApplicationUser applicationUser = usersRepository.GetById(id);
            if (applicationUser != null)
            {
                imageRepository.DeleteImageInWroot(applicationUser.Image);
            }
            usersRepository.Delete(id);
            if (ReturnToBranch)
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            else if (RetuenToWorkers)
                return RedirectToAction("GetAllWorkers");
            return RedirectToAction("GetAllAccountants");
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteAdmin(string id, bool ReturnToBranch = false, int BranchId = 0, bool RetuenToWorkers = false)
        {
                        ApplicationUser applicationUser=usersRepository.GetById(id);
            if (applicationUser != null)
            {
                imageRepository.DeleteImageInWroot(applicationUser.Image);
            }
            usersRepository.Delete(id);
            if (ReturnToBranch)
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            else if (RetuenToWorkers)
                return RedirectToAction("GetAllWorkers");
            return RedirectToAction("GetAllAdmins");
        }
        #endregion

    }
}

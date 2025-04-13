using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    public class BranchGusetServiceController : Controller
    {
        IGeneralRepository<BranchGusetService> generalRepository;
        IGeneralRepository<DateBranch> datebranchgeneralRepository;
        private readonly IBranchGusetServiceBz branchGusetServiceBz;
        private readonly ISortAndSearchRepository<BranchGusetService> sortAndSearchRepository;
        private readonly IWorkerRepository<Admin> workerRepository;
        private readonly IWorkerRepository<Assastant> workerRepositoryAssastant;
        private readonly IWorkerRepository<Doctor> workerRepositoryDoctor;
        IBranchGusetServiceRepository branchGusetRepository;

        public BranchGusetServiceController(IGeneralRepository<BranchGusetService> _generalRepository
            , IBranchGusetServiceRepository branchGusetRepository
            ,IGeneralRepository<DateBranch> _datebranchgeneralRepository,
            IBranchGusetServiceBz _branchGusetServiceBz,
            ISortAndSearchRepository<BranchGusetService> sortAndSearchRepository
            ,IWorkerRepository<Admin> workerRepository,
            IWorkerRepository<Assastant> workerRepositoryAssastant,
            IWorkerRepository<Doctor> workerRepositoryDoctor)
        {
            this.generalRepository = _generalRepository;
            this.branchGusetRepository = branchGusetRepository;
            this.datebranchgeneralRepository = _datebranchgeneralRepository;
            branchGusetServiceBz = _branchGusetServiceBz;
            this.sortAndSearchRepository = sortAndSearchRepository;
            this.workerRepository = workerRepository;
            this.workerRepositoryAssastant = workerRepositoryAssastant;
            this.workerRepositoryDoctor = workerRepositoryDoctor;
        }
        [Authorize(Roles = "Admin,SuperAdmin,Assastant,Doctor")]
        public IActionResult Index(int CurrentPage=1,string Sort="")
        {
            List<BranchGusetService> BGS;
            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (User.IsInRole("Admin"))
            {
                Admin admin = workerRepository.GetByUserId02(userid);
                BGS = sortAndSearchRepository.StateSort(Sort,admin?.BranchID??0);
                ViewBag.Admin = admin;
            }
            else if (User.IsInRole("Assastant"))
            {
                Assastant assastant = workerRepositoryAssastant.GetByUserId02(userid);
                BGS = sortAndSearchRepository.StateSort(Sort, assastant?.BranchID ?? 0);
                ViewBag.Assastant = assastant;
            }
            else if (User.IsInRole("Doctor"))
            {
                Doctor doctor = workerRepositoryDoctor.GetByUserId02(userid);
                BGS = sortAndSearchRepository.StateSort(Sort, doctor?.BranchID ?? 0);
                ViewBag.Doctor = doctor;
            }
            else
            {
                BGS = sortAndSearchRepository.StateSort(Sort);
            }

            PaginationVm<BranchGusetService> paginationVm = new PaginationVm<BranchGusetService>(5, CurrentPage, BGS);
            ViewBag.Pagination = paginationVm;
            BGS = paginationVm.Items;
            ViewBag.Sort = Sort;
            return View(BGS);
        }
        public IActionResult GetById(int id) { 
            return View(generalRepository.GetById(id));
        }
        public IActionResult DeleteGuset(int bgsid,int gusetid,int datebranchid) {
            generalRepository.Delete(bgsid);
            DateBranch datebranch = datebranchgeneralRepository.GetById(datebranchid); 
            branchGusetServiceBz.ReturnDateState(datebranch, datebranchid);
            return RedirectToAction("RelatedBGS", "Guset", new {id=gusetid});
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteAdmin(int gusetid, int datebranchid, int id
            , bool ReturnToBranch = false
            , int BranchId = 0)
        {

            generalRepository.Delete(id);
            DateBranch datebranch = datebranchgeneralRepository.GetById(datebranchid);
            branchGusetServiceBz.ReturnDateState(datebranch, datebranchid);
            if (ReturnToBranch)
            {
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            }
            return RedirectToAction("Index");
        }

        public IActionResult RelatedOrdersForCertainBranch(int gusetId,int branchid=0) { 
        
            List<MedicalService> services= branchGusetRepository.RelatedOrdersForCertainBranch(gusetId, branchid);
            #region Mapping
            List<MedicalServicesForDropDownVm> medicalServicesVm= new List<MedicalServicesForDropDownVm>();

            foreach (MedicalService service in services) {
                MedicalServicesForDropDownVm medicalServiceVm = new MedicalServicesForDropDownVm();
                medicalServiceVm.Name = service?.Name??"Not Found";
                medicalServiceVm.branchId = service?.BranchID ?? 0;
                medicalServiceVm.ID = service?.ID??0;
                medicalServicesVm.Add(medicalServiceVm);

            }
            #endregion

            return Ok(medicalServicesVm);
        }


        public IActionResult SearchByName(string searchname,int branchid=0)
        {
            List<BranchGusetService> branchGusetServices = sortAndSearchRepository.SearchByGuset(searchname,branchid);
            return PartialView(branchGusetServices);
        }





    }
}

using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    public class MedicalServiceController : Controller
    {
        #region Ctor
        IGeneralRepository<MedicalService> generalRepository;
        IGeneralRepository<Catigory> generalCatigory;
        private readonly IGeneralRepository<Branch> generalBranch;
        private readonly ISortAndSearchRepository<MedicalService> sortAndSearchRepository;
        private readonly IWorkerRepository<Admin> workerRepository;
        private readonly IImageRepository imageRepository;
        private readonly IMedicalServicesRepository medical;

        public MedicalServiceController(IGeneralRepository<MedicalService> _generalRepository,
            IGeneralRepository<Catigory> _generalCatigory,
            IGeneralRepository<Branch> _generalBranch,
            ISortAndSearchRepository<MedicalService> sortAndSearchRepository,
            IWorkerRepository<Admin> workerRepository,
            IImageRepository imageRepository,
            IMedicalServicesRepository medical
            
            )
        {
            generalRepository = _generalRepository;
            generalCatigory=_generalCatigory;
            generalBranch = _generalBranch;
            this.sortAndSearchRepository = sortAndSearchRepository;
            this.workerRepository = workerRepository;
            this.imageRepository = imageRepository;
            this.medical = medical;
        } 
        #endregion

        #region Related
        public IActionResult GetAll()
        {
            return View(generalRepository.GetAll());
        }
        public IActionResult GetById(int id)
        {
            return View(generalRepository.GetById(id));
        }
        public IActionResult GetByIdAdmin(int id)
        {
            return View(generalRepository.GetById(id));
        }
        #endregion


        #region CRUD

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Insert(int CurrentPage=1,string Sort="") {
            InsertMedicalServicesCollectionVm vm = new InsertMedicalServicesCollectionVm();
           vm.branches=new List<Branch>();

            if (User.IsInRole("Admin"))
            {
                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Admin admin = workerRepository.GetByUserId02(userid);
                Branch branch = admin.Branch;
                vm.MedicalServices = sortAndSearchRepository.StateSort(Sort, admin?.BranchID ?? 0);
                vm.branches.Add(branch);    
                ViewBag.Admin = admin;
            }
            else
            {
                vm.MedicalServices = sortAndSearchRepository.StateSort(Sort);
                vm.branches = generalBranch.GetAll();
            }
            vm.Catigorys= generalCatigory.GetAll();
            PaginationVm<MedicalService> paginationVm = new PaginationVm<MedicalService>(5,CurrentPage
                ,vm.MedicalServices);
            vm.MedicalServices = paginationVm.Items;
            ViewBag.Pagination = paginationVm;  
            ViewBag.Collection= vm;
            ViewBag.Sort = Sort;

            return View();
        }
        #region Old Insert
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Insert(MedicalServiceVm medicalServiceVm, int LastPage)
        {

            if (ModelState.IsValid)
            {
                MedicalService medicalService = new MedicalService();
                InsertMedicalServicesCollectionVm vm = new InsertMedicalServicesCollectionVm();
                vm.branches = generalBranch.GetAll();
                vm.MedicalServices = generalRepository.GetAll();
                vm.Catigorys = generalCatigory.GetAll();
                ViewBag.Collection = vm;
                await imageRepository.SaveImageInWroot(medicalService, medicalServiceVm.Image);
                medicalService.Name = medicalServiceVm.Name;
                medicalService.Price = medicalServiceVm.Price;
                medicalService.Description = medicalServiceVm.Description;
                medicalService.BranchID = medicalServiceVm.BranchId;
                medicalService.CatigoryID = medicalServiceVm.CatId;
                generalRepository.Insert(medicalService);
                return RedirectToAction("Insert", new { CurrentPage = LastPage });
            }

            return View(medicalServiceVm);
        }
        #endregion




        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult UpdateById(int id,int CurrentPage, string Sort)
        {
            InsertMedicalServicesCollectionVm vm= new InsertMedicalServicesCollectionVm();
            vm.branches=generalBranch.GetAll();
            vm.Catigorys=generalCatigory.GetAll();
            ViewBag.Collection = vm;
            ViewBag.CurrentPage=CurrentPage;    
            ViewBag.Sort=Sort;
            ViewBag.Id = id;

            #region Mapping
            MedicalService medicalService = generalRepository.GetById(id);
            MedicalServiceVm medicalServiceVm = new MedicalServiceVm();
            medicalServiceVm.Name=medicalService.Name;
            medicalServiceVm.Price= medicalService.Price;
            medicalServiceVm.CatId=medicalService?.CatigoryID??0;
            medicalServiceVm.BranchId=medicalService?.BranchID??0;
            medicalServiceVm.Description=medicalService.Description;

            #endregion

            return PartialView(medicalServiceVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public  IActionResult Update(int id,MedicalServiceVm medicalServiceVm,int CurrentPage,string Sort)
        {
            MedicalService OldService=generalRepository.GetById(id);
            MedicalService NewService =new MedicalService();
            NewService.Name=medicalServiceVm.Name;
            NewService.Price=medicalServiceVm.Price;
            NewService.BranchID=medicalServiceVm.BranchId;
            NewService.CatigoryID=medicalServiceVm.CatId;
            NewService.Description=medicalServiceVm.Description;
            imageRepository.UpdateImageInWroot(OldService.Image, NewService, medicalServiceVm.Image);
            medical.Update(id, NewService);
            return RedirectToAction("Insert",new { CurrentPage=CurrentPage,Sort=Sort});
        }
        #region MyRegion
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Delete(int id, bool ReturnToBranch = false, int BranchId = 0)
        {
            var medicalService = generalRepository.GetById(id);

            if (medicalService != null)
            {
                imageRepository.DeleteImageInWroot(medicalService.Image);
                generalRepository.Delete(id);

                if (ReturnToBranch)
                {
                    return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
                }
                return RedirectToAction("Insert");
            }

            return NotFound();
        }
        #endregion

        #endregion
        public IActionResult SearchByName(string searchname,int branchid)
        {
            List<MedicalService> result = sortAndSearchRepository.SearchByName(searchname, branchid);
            return PartialView(result);

        }
        public IActionResult SearchByNameGuset(string searchname, int branchid)
        {
            List<MedicalService> result = sortAndSearchRepository.SearchByName(searchname, branchid);
            return PartialView(result);

        }

    }
}

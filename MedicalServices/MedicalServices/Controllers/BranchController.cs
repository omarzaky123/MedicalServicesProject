using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;

namespace MedicalServices.Controllers
{
    public class BranchController : Controller
    {
        #region Ctor
        private readonly IGeneralRepository<Branch> branchRepository;
        private readonly IBranchRepository branchRepositoryNonGeneral;
        private readonly ISortAndSearchRepository<Branch> sortAndSearchRepository;
        private readonly IImageRepository imageRepository;

        public BranchController(IGeneralRepository<Branch> _branchRepository
            , IBranchRepository branchRepositoryNonGeneral,
            ISortAndSearchRepository<Branch> sortAndSearchRepository,IImageRepository imageRepository)
        {
            branchRepository = _branchRepository;
            this.branchRepositoryNonGeneral = branchRepositoryNonGeneral;
            this.sortAndSearchRepository = sortAndSearchRepository;
            this.imageRepository = imageRepository;
        } 
        #endregion

        #region Related
        public IActionResult RelatedService(int id)
        {
            return PartialView(branchRepositoryNonGeneral.GetRelatedMedical(id));
        }
        [Authorize(Roles = "Admin,SuperAdmin,Accountant")]
        public IActionResult GetByIdAdmin(int id)
        {
            GetByAdminBranchCollectionVm vm= new GetByAdminBranchCollectionVm();
            vm.RelatedServices= branchRepositoryNonGeneral.GetRelatedMedical(id);
            vm.Catigorys = branchRepositoryNonGeneral.GetRelateCatigorys(id);
            vm.RelatedBGS= branchRepositoryNonGeneral.GetRelatedBGS(id);
            vm.RelatedDoctors = branchRepositoryNonGeneral.GetRelatedDoctors(id);
            vm.RelatedAdmins= branchRepositoryNonGeneral.GetRelatedAdmins(id);
            vm.RelatedAssastants= branchRepositoryNonGeneral.GetRelatedAssastant(id);
            vm.RelatedAccountants = branchRepositoryNonGeneral.GetRelatedAccounts(id);
            vm.TotalGain= branchRepositoryNonGeneral.TotalGain(id);
            vm.TodayGain= branchRepositoryNonGeneral.TodayGain(id);
            vm.TotalSalaryForEmployees= branchRepositoryNonGeneral.TotalSalaryForEmployee(id);
            vm.TotalGainThisMonth= branchRepositoryNonGeneral.TotalGainForMonth(id);
            vm.NetTotalGainThisMonth = branchRepositoryNonGeneral.NetTotalGainForMonth(id);
            ViewBag.Collection = vm;
            return View(branchRepository.GetById(id));
        }

        #endregion

        #region CRUD
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Insert(int CurrentPage=1,string Sort="")
        {
            List<Branch> branches;
            branches=sortAndSearchRepository.StateSort(Sort);
            PaginationVm<Branch> paginationVm = new PaginationVm<Branch>(2,CurrentPage,branches);
            ViewBag.Branches= paginationVm.Items;
            ViewBag.Pagination = paginationVm;
            ViewBag.Sort = Sort;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Insert(BranchVm branchVm,int LastPage)
        {
            if (ModelState.IsValid)
            {
                Branch branch = new Branch();
                branch.Name=branchVm.Name;  
                branch.Location=branchVm.Location;
                branch.ContactPhone= branchVm.ContanctPhone;
                imageRepository.SaveImageInWroot(branch, branchVm.Image);
                branchRepository.Insert(branch);    
                ViewBag.Branches = branchRepository.GetAll();
                return RedirectToAction("Insert",new {CurrentPage=LastPage});   
            }
           
            return View(branchVm);
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UpdateById(int id,int CurrentPage, string Sort)
        {
            ViewBag.CurrentPage=CurrentPage;
            ViewBag.Sort=Sort;
            return PartialView(branchRepository.GetById(id));
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update(int id,Branch branch,int CurrentPage,string Sort)
        {
            branchRepository.Update(id,branch);
            return RedirectToAction("Insert", new {CurrentPage=CurrentPage,Sort=Sort});
        }


        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id,int CurrentPage=0,string Sort="") {
            Branch branch = branchRepository.GetById(id);
            imageRepository.DeleteImageInWroot(branch.Image);
            branchRepositoryNonGeneral.Delete(id);
            return RedirectToAction("Insert",new {CurrentPage=CurrentPage,Sort=Sort});
        }

        #endregion
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SearchByName(string searchname) { 
            
            List<Branch> branches = sortAndSearchRepository.SearchByName(searchname);
            return PartialView(branches);
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult BranchName(int id) {

            return Json(branchRepositoryNonGeneral.BranchName(id));
        }
        [Authorize(Roles = "Admin,SuperAdmin,Accountant")]
        public IActionResult LastMonthesGain(int id) {

            List<decimal> gains=new List<decimal>();
            for (int i = -2; i <= 0; i++) {
                gains.Add(branchRepositoryNonGeneral.TotalGainForMonth(id, i));
            }
            return Json(gains);  
        }
        [Authorize(Roles = "Admin,SuperAdmin,Accountant")]
        public IActionResult LastMonthesNet(int id)
        {

            List<decimal> gains = new List<decimal>();
            for (int i = -2; i <= 0; i++)
            {
                gains.Add(branchRepositoryNonGeneral.NetTotalGainForMonth(id, i));
            }
            return Json(gains);
        }
        [Authorize(Roles = "Admin,SuperAdmin,Accountant")]
        public IActionResult GetMonths()
        {
            var months = new List<string>();
            var englishCulture = new CultureInfo("en-US"); // Force English names

            for (int i = -2; i <= 0; i++)
            {
                var date = DateTime.Today.AddMonths(i);
                string monthName = englishCulture.DateTimeFormat.GetMonthName(date.Month);
                months.Add(monthName);
            }

            return Json(months);
        }
        [Authorize(Roles = "Admin,SuperAdmin,Accountant")]
        public IActionResult GetMostServices(int id)
        {
            var services = branchRepositoryNonGeneral.MostRequiredService(id);
            var result = services.Select(s => new 
            {
                ServicesName = s.serviceName,
                ServicesCount = s.Count
            }).ToList();
            return Json(result);
        }
    }
}

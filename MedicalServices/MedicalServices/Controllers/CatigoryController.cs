using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MedicalServices.Controllers
{
    
    public class CatigoryController : Controller
    {
        #region Ctor
        private readonly ICatigoryRepository catigoryRepository;
        private readonly IGeneralRepository<Catigory> generalRepository;
        private readonly ISortAndSearchRepository<Catigory> sortAndSearchRepository;

        public CatigoryController(ICatigoryRepository catigoryRepository,
            IGeneralRepository<Catigory> _generalRepository,ISortAndSearchRepository<Catigory> sortAndSearchRepository)
        {
            this.catigoryRepository = catigoryRepository;
            generalRepository = _generalRepository;
            this.sortAndSearchRepository = sortAndSearchRepository;
        } 
        #endregion

        #region ActionCustom
        public IActionResult RelatedServiceBranch(int BranchId, int CatigoryId)
        {
            return PartialView(catigoryRepository.GetRelatedServicesBranch(BranchId, CatigoryId));
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetByIdAdmin(int id)
        {
            return View(generalRepository.GetById(id));
        }
        #endregion

        #region CRUD
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UpdateById(int id,int CurrentPage,string Sort)
        {
            ViewBag.CurrentPage = CurrentPage;
            ViewBag.Sort=Sort;
            ViewBag.Id = id;
            #region Mapping
            Catigory catigory = generalRepository.GetById(id);
            CatigoryVm catigoryVm=new CatigoryVm();
            catigoryVm.Name = catigory.Name;
            catigoryVm.Description = catigory.Description;
            #endregion
            return PartialView(catigoryVm);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Insert(int CurrentPage=1,string Sort="")
        {
            List<Catigory> catigories;
            catigories= sortAndSearchRepository.StateSort(Sort);
            PaginationVm<Catigory> pagination = new PaginationVm<Catigory>(5,CurrentPage,catigories);
            ViewBag.Catigorys = pagination.Items;
            ViewBag.Pagination=pagination;
            ViewBag.Sort=Sort;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Insert(CatigoryVm catigoryVm,int LastPage)
        {
            if (ModelState.IsValid)
            {
                Catigory catigory = new Catigory();
                catigory.Name = catigoryVm.Name;
                catigory.Description = catigoryVm.Description;
                generalRepository.Insert(catigory);
                ViewBag.Catigorys = generalRepository.GetAll();
                return RedirectToAction("Insert",new { CurrentPage= LastPage});
            }
            return View(catigoryVm);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update(int id, CatigoryVm catigoryVm,int CurrentPage=1,string Sort="")
        {
            if (ModelState.IsValid)
            {
                Catigory catigoryMapp = new Catigory();
                catigoryMapp.Name = catigoryVm.Name;
                catigoryMapp.Description = catigoryVm.Description;
                generalRepository.Update(id, catigoryMapp);
                return Json(new { success = true });
                //return RedirectToAction("Insert",new {CurrentPage=CurrentPage ,Sort=Sort});
            }
            return PartialView("UpdateById", catigoryVm);
            //return RedirectToAction("Insert");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id, bool ReturnToBranch = false, int BranchId = 0,string Sort=""
            ,int CurrentPage=0)
        {
            generalRepository.Delete(id);
            if (ReturnToBranch)
            {
                return RedirectToAction("GetByIdAdmin", "Branch", new { id = BranchId });
            }
            return RedirectToAction("Insert",new {CurrentPage=CurrentPage,Sort=Sort});
        }

        #endregion



        public IActionResult SearchByName(string searchname)
        {
            List<Catigory> result = sortAndSearchRepository.SearchByName(searchname);
            return PartialView(result);

        }
    }
}

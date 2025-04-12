using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    public class DateController : Controller
    {
        private readonly IDateRepository dateRepository;
        private readonly IGeneralRepository<Branch> generalbranchRepository;
        private readonly IWorkerRepository<Admin> workerRepository;
        private readonly IWorkerRepository<Assastant> workerRepositoryAssastant;

        public DateController(IDateRepository dateRepository,
            IGeneralRepository<Branch> generalbranchRepository,
            IWorkerRepository<Admin> workerRepository,
            IWorkerRepository<Assastant> workerRepositoryAssastant)
        {
            this.dateRepository = dateRepository;
            this.generalbranchRepository = generalbranchRepository;
            this.workerRepository = workerRepository;
            this.workerRepositoryAssastant = workerRepositoryAssastant;
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult GetDates() {  
           return View(dateRepository.GetDates());
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UpdateDates()
        {
            List<Date> dates = dateRepository.UpdateDates();
            return RedirectToAction("GetDates",dates);
        }
        [Authorize(Roles = "SuperAdmin,Admin,Assastant")]

        public IActionResult AssignDatesForBranch() {

            AssignDatesForBranchVm assignDatesForBranchVm = new AssignDatesForBranchVm();
            assignDatesForBranchVm.Branches=new List<Branch>();
            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (User.IsInRole("Admin"))
            {
                Admin admin = workerRepository.GetByUserId02(userid);
                assignDatesForBranchVm.Branches.Add(admin.Branch);
            }
            else if (User.IsInRole("Assastant"))
            {
                Assastant assastant = workerRepositoryAssastant.GetByUserId02(userid);
                assignDatesForBranchVm.Branches.Add(assastant.Branch);
            }
            else
            {
                assignDatesForBranchVm.Branches = generalbranchRepository.GetAll();
            }
            assignDatesForBranchVm.Dates=dateRepository.GetDates();
            return View(assignDatesForBranchVm);
        }
        public IActionResult SaveAssignDatesForBranch(int branchid,int dateid) { 
            dateRepository.AddDateForBranch(branchid,dateid);
            return Json(new { success = true, message = "Date assigned successfully!" });
        }
        public IActionResult DeleteAllDatesBranches()
        {
            dateRepository.DeleteAllDateForBranch();
            return Json(new { success = true, message = "Date assigned successfully!" });
        }
        public IActionResult DeleteDatesForBranch(int branchid) {
        
            dateRepository.DeleteDateForBranch(branchid);
            return Json(new { success = true, message = "Date assigned successfully!" });
        }
        public IActionResult DateLocalStorge() {
        
            return View();
        }

        public IActionResult DateName(int id)
        {
            return Json(dateRepository.DateName(id));
        }


    }
}

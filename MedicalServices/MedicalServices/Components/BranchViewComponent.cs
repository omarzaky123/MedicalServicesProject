using MedicalServices.Models;
using MedicalServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http; // Add this if not already present

namespace MedicalServices.Component
{
    public class BranchViewComponent : ViewComponent
    {
        private readonly IGeneralRepository<Branch> generalRepositorys;
        private readonly IWorkerRepository<Accountant> workerRepository;

        public BranchViewComponent(IGeneralRepository<Branch> _generalRepositorys,
            IWorkerRepository<Accountant> workerRepository)
        {
            generalRepositorys = _generalRepositorys;
            this.workerRepository = workerRepository;
        }

        public IViewComponentResult Invoke()
        {
            List<Branch> branches = new List<Branch>();
            var user = HttpContext.User as ClaimsPrincipal; // Cast to ClaimsPrincipal

            if (user.IsInRole("SuperAdmin"))
            {
                branches = generalRepositorys.GetAll();
            }
            else if (user.IsInRole("Accountant"))
            {
                string userid = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userid != null)
                {
                    Accountant accountant = workerRepository.GetByUserId02(userid);
                    branches.Add(accountant.Branch);
                }
            }
            return View(branches);
        }
    }
}
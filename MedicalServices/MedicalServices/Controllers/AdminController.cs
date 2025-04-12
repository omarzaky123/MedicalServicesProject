using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISortAndSearchRepository<Admin> sortAndSearchRepository;

        public AdminController(ISortAndSearchRepository<Admin> sortAndSearchRepository)
        {
            this.sortAndSearchRepository = sortAndSearchRepository;
        }

        public IActionResult SearchByName(string searchname)
        {
            ViewBag.Detail = "AdminDetails";
            ViewBag.Delete = "DeleteAdmin";
            List<Admin> admins = sortAndSearchRepository.SearchWorkerByName(searchname);
            return PartialView("SearchByNameCertainJob", admins.Cast<IWorker>().ToList());
        }
    }
}

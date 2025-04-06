using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class AccountantController : Controller
    {
        private readonly ISortAndSearchRepository<Accountant> sortAndSearchRepository;

        public AccountantController(ISortAndSearchRepository<Accountant> sortAndSearchRepository)
        {
            this.sortAndSearchRepository = sortAndSearchRepository;
        }

        public IActionResult SearchByName(string searchname)
        {
            ViewBag.Detail = "AccountantDetails";
            ViewBag.Delete = "DeleteAccoutant";
            List<Accountant> accountants = sortAndSearchRepository.SearchWorkerByName(searchname);
            return PartialView("SearchByNameCertainJob", accountants.Cast<IWorker>().ToList());
        }
    }
}

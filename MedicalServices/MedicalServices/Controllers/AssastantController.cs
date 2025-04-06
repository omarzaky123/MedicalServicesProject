using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class AssastantController : Controller
    {
        private readonly ISortAndSearchRepository<Assastant> sortAndSearchRepository;

        public AssastantController(ISortAndSearchRepository<Assastant> sortAndSearchRepository)
        {
            this.sortAndSearchRepository = sortAndSearchRepository;
        }

        public IActionResult SearchByName(string searchname)
        {
            ViewBag.Detail = "AssastantDetails";
            ViewBag.Delete = "DeleteAssastant";
            List<Assastant> assastants = sortAndSearchRepository.SearchWorkerByName(searchname);
            return PartialView("SearchByNameCertainJob", assastants.Cast<IWorker>().ToList());
        }
    }
}

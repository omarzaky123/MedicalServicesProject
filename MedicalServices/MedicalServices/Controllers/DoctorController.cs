using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ISortAndSearchRepository<Doctor> sortAndSearchRepository;

        public DoctorController(ISortAndSearchRepository<Doctor> sortAndSearchRepository)
        {
            this.sortAndSearchRepository = sortAndSearchRepository;
        }

        public IActionResult SearchByName(string searchname) {
            ViewBag.Detail = "DoctorDetails";
            ViewBag.Delete = "DeleteDoctor";
            List<Doctor> doctors=sortAndSearchRepository.SearchWorkerByName(searchname);
            return PartialView("SearchByNameCertainJob", doctors.Cast<IWorker>().ToList());
        }
    }
}

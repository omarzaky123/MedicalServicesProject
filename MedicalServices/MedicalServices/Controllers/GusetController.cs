using MedicalServices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class GusetController : Controller
    {
        IGusetRepository gusetRepository;
        public GusetController(IGusetRepository _gusetRepository)
        {
            this.gusetRepository = _gusetRepository;
        }
        [Authorize]
        public IActionResult RelatedBGS(int id)
        {
            return View(gusetRepository.RelatedBGS(id));
        }
    }
}

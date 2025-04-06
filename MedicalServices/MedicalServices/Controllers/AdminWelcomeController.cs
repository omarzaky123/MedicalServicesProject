using Microsoft.AspNetCore.Mvc;

namespace MedicalServices.Controllers
{
    public class AdminWelcomeController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }
    }
}

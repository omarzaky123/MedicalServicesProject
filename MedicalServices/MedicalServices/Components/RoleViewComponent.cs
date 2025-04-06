using MedicalServices.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MedicalServices.Components
{
    public class RoleViewComponent: ViewComponent
    {
        private readonly MyContext myContext;

        public RoleViewComponent(MyContext _myContext)
        {
            myContext = _myContext;
        }


        public IViewComponentResult Invoke()
        {
            List<IdentityRole> Roles = myContext.Roles.Where(R => (R.Name != "Guset") &&(R.Name!="SuperAdmin")).ToList();
            return View(Roles);
        }
    }
}

using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;  // Add this line
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;


namespace MedicalServices.Controllers
{
    public class WelcomeController : Controller
    {
        #region Intial
        private readonly IGeneralRepository<Branch> generalbranchRepository;
        private readonly IGeneralRepository<MedicalService> generalMedicalRepository;
        private readonly IGeneralRepository<BranchGusetService> BGSGeneralRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IGusetRepository gusetRepository; 
        private readonly IGeneralRepository<DateBranch> dateBranchgeneralRepository;
        private readonly IGeneralRepository<Doctor> generaldoctorRepository;
        #endregion

        #region Ctor
        public WelcomeController(IGeneralRepository<Branch> _generalbranchRepository
            , IBranchRepository _branchRepository,
            IGeneralRepository<MedicalService> _generalMedicalRepository,
            IGusetRepository _gusetRepository,
            IGeneralRepository<BranchGusetService> _BGSGeneralRepository,
            IGeneralRepository<DateBranch> _dateBranchgeneralRepository,
            IGeneralRepository<Doctor> generaldoctorRepository)
        {
            this.generalbranchRepository = _generalbranchRepository;
            this.branchRepository = _branchRepository;
            this.generalMedicalRepository = _generalMedicalRepository;
            this.gusetRepository = _gusetRepository;
            this.BGSGeneralRepository = _BGSGeneralRepository;
            //this.dateRepository= _dateRepository;
            this.dateBranchgeneralRepository = _dateBranchgeneralRepository;
            this.generaldoctorRepository = generaldoctorRepository;
        }
        #endregion

        #region Pages
        public IActionResult WelcomePage()
        {
            WelcomePageAllNeeds welcomePageAllNeeds = new WelcomePageAllNeeds();
            welcomePageAllNeeds.branches = generalbranchRepository.GetAll();
            welcomePageAllNeeds.doctors= generaldoctorRepository.GetAll();
            return View(welcomePageAllNeeds);
        }

        public IActionResult SeeMedicalServcie(int id)

        {
            ViewBag.BranchCatigorys= branchRepository.GetRelateCatigorys(id);
            return View(branchRepository.GetRelatedMedical(id));
        }
        public IActionResult StoreGusetCookie(int MedicalId, int BranchId)
        {
            TempData["BranchIdCookie"] = BranchId;
            TempData["MedicalServiceCookie"] = MedicalId;
            return RedirectToAction("Invoice");
        } 
        #endregion

        #region Invoice
        [Authorize]
        public IActionResult Invoice()
        {
            InvoiceVm invoiceVm = new InvoiceVm();
            Branch branch = new Branch();
            MedicalService service = new MedicalService();
            string name = User.Identity.Name;
            if (TempData.ContainsKey("BranchIdCookie"))
                branch = generalbranchRepository.GetById((int)TempData.Peek("BranchIdCookie"));
            if (TempData.ContainsKey("MedicalServiceCookie"))
                service = generalMedicalRepository.GetById((int)TempData.Peek("MedicalServiceCookie"));
            invoiceVm.BranchLocation = branch.Location;
            invoiceVm.MedicalServicePrice = service.Price;
            invoiceVm.MedicalServiceName = service.Name;
            invoiceVm.BranchName = branch.Name;
            invoiceVm.GusetName = name;
            ViewBag.Dates = branchRepository.GetRelatedAvilableDates(branch.ID);
            return View(invoiceVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Invoice(InvoiceVm invoiceVm)
        {
            try
            {
                BranchGusetService BGservice = new BranchGusetService();
                int branchId = 0;
                int serviceId = 0;
                if (TempData.ContainsKey("BranchIdCookie"))
                    branchId = (int)TempData.Peek("BranchIdCookie");
                if (TempData.ContainsKey("MedicalServiceCookie"))
                    serviceId = (int)TempData.Peek("MedicalServiceCookie");
                BGservice.DateBranchID = invoiceVm.AppointmentDateID;
                dateBranchgeneralRepository.GetById((int)invoiceVm.AppointmentDateID).State=false;
                BGservice.ServiceID = serviceId;
                BGservice.BranchID = branchId;
                string Userid =
                    User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Guset guset = gusetRepository.AccountId(Userid);
                BGservice.GusetID = guset.ID;
                BGSGeneralRepository.Insert(BGservice);
                return RedirectToAction("RelatedBGS", "Guset", new { id = guset.ID });
            }
            catch (Exception ex) {
                return View("BranchGusetServiceError");
            }

        } 
        #endregion

    }
}


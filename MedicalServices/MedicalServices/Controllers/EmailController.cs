using Humanizer;
using MedicalServices.Emali;
using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MedicalServices.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Doctor")]

    public class EmailController : Controller
    {
        private readonly IMailingService _mailingService;
        private readonly IBranchGusetServiceRepository branchGusetService;

        public EmailController(IMailingService mailingService,IBranchGusetServiceRepository branchGusetService)
        {
            _mailingService = mailingService;
            this.branchGusetService = branchGusetService;
        }
        [HttpGet]
        public IActionResult FillEmail(string email, int Bgs, string defaultsubj
            , string defaultbody,string Attatchment=null)
        {
            
            TempData["email"]=email;
            TempData["defaultsubj"] =defaultsubj;
            TempData["defaultbody"] = defaultbody;
            TempData["BgsId"]=Bgs;
            if(Attatchment!=null)
                TempData["Attatchments"] =Attatchment;
            return RedirectToAction("SendEmail");
        }
        [HttpGet]
        public IActionResult SendEmail(bool FromMenu=false)
        {
            TempData["FromMenu"]=FromMenu;
            if(TempData["email"] != null)   
                ViewBag.email=TempData["email"];
            if (TempData["defaultsubj"] != null)
                ViewBag.defaultsubj = TempData["defaultsubj"];
            if (TempData["defaultbody"] != null)
                ViewBag.defaultbody = TempData["defaultbody"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendEmail(EmailVm emailVm)
        {
            await _mailingService.SendEmailAsync(emailVm.ToEmail, emailVm.Subject, emailVm.Body, emailVm.Attachments);
            
            bool FromMenu = (bool)TempData["FromMenu"];
            if (TempData["BgsId"] is int Bgsid && TempData["BgsId"]!=null && FromMenu==false)
            {
                branchGusetService.SetEamilSent(Bgsid);
            }
            TempData["success"] = "Email Sent Successfully";

            return RedirectToAction("Index","File");
        }
        
    }
}

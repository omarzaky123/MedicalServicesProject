using MedicalServices.Models;
using MedicalServices.Repository;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Numerics;
using System.Security.Claims;

namespace MedicalServices.Controllers
{
    [Authorize(Roles = "SuperAdmin,Doctor")]
    public class FileController : Controller
    {
        private readonly IFileRepository fileRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBranchRepository branchRepository;
        private readonly IWorkerRepository<Doctor> workerRepositoryDoc;
        private readonly IGeneralRepository<BranchGusetService> generalRepositoryBGS;
        private readonly IBranchGusetServiceRepository branchGusetService;

        public FileController(IFileRepository fileRepository,
            IWebHostEnvironment webHostEnvironment,
            IBranchRepository branchRepository,
            IWorkerRepository<Doctor> workerRepositoryDoc,
            IGeneralRepository<BranchGusetService> generalRepositoryBGS,
            IBranchGusetServiceRepository branchGusetService)
        {
            this.fileRepository = fileRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.branchRepository = branchRepository;
            this.workerRepositoryDoc = workerRepositoryDoc;
            this.generalRepositoryBGS = generalRepositoryBGS;
            this.branchGusetService = branchGusetService;
        }
        public IActionResult Index()
        {
            List<UplodedFile> files=new List<UplodedFile>();
            if (User.IsInRole("Doctor"))
            {
                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Doctor doctor = workerRepositoryDoc.GetByUserId02(userid);
                files = fileRepository.GetAllForCertainBranch(doctor?.BranchID??0);
            }
            else if(User.IsInRole("SuperAdmin"))
            {
                files = fileRepository.GetAll();
            }
           
            return View(files);
        }
        [HttpGet]
        public IActionResult Upload()
        {
            if (User.IsInRole("Doctor"))
            {
                string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                Doctor doctor = workerRepositoryDoc.GetByUserId02(userid);
                ViewBag.BGS = branchGusetService.GetForCertainBranchNotUplodedOrders(doctor?.BranchID??0);
            }
            else if (User.IsInRole("SuperAdmin"))
            {
                ViewBag.BGS = branchGusetService.GetForCertainBranchNotUplodedOrders();
            }

            return View();  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(UploadedFilesVm uploadedFilesVm)
        {
            List<UplodedFile> files = new List<UplodedFile>();
            foreach (var fileVm in uploadedFilesVm.Files) {
                var fakeFileName = Path.GetRandomFileName();

                UplodedFile file = new UplodedFile()
                {
                    FileName = fileVm.FileName,
                    ContentType = fileVm.ContentType,
                    StoredFileName = fakeFileName,
                    BGSID = uploadedFilesVm.OrderForignKeyID


                };

                var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploades", fakeFileName);

                using FileStream fileStream = new FileStream(path, FileMode.Create);
                fileVm.CopyTo(fileStream);
                files.Add(file);

            }
            
            fileRepository.InsertRange(files);
            branchGusetService.SetUploade(uploadedFilesVm.OrderForignKeyID);

            return RedirectToAction("Index");
        }

        public IActionResult Download(string fileName)
        {
            var UploadedFile= fileRepository.GetByStoredName(fileName);
            if(UploadedFile == null)
            {
                return NotFound();
            }
            var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploades", fileName);
            MemoryStream memoryStream = new MemoryStream(); 
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0;

            return File(memoryStream,UploadedFile.ContentType,UploadedFile.FileName);
        }
        public IActionResult Delete(string fileName,int BGS=0)
        {
            var uploadedFile = fileRepository.GetByStoredName(fileName);
            if (uploadedFile == null)
            {
                return NotFound();
            }
            if (BGS != 0)
            {
                BranchGusetService branchGusetService= generalRepositoryBGS.GetById(BGS);
                branchGusetService.Uploaded = false;
                branchGusetService.EmailSent = false;
            }

            fileRepository.Delete(fileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploades", fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Index");
        }
    }
}

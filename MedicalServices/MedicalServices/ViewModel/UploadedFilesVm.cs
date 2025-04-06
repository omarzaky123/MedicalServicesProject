using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class UploadedFilesVm
    {
        [Required]
        public List<IFormFile> Files { get; set; }
        [Required]
        public int OrderForignKeyID { get; set; }

    }
}

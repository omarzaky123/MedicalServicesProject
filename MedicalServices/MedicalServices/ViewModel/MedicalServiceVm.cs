using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    #region OldOne
    public class MedicalServiceVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public IFormFile  Image { get; set; }
        [Required]
        public int CatId { get; set; }

    }
    #endregion


}

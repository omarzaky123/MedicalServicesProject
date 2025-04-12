using System.ComponentModel.DataAnnotations;

namespace MedicalServices.ViewModel
{
    public class InvoiceVm
    {
        [Required]
        public string GusetName { get; set; }
        [Required]
        public string  MedicalServiceName { get; set; }
        [Required]
        public string BranchName { get; set; }
        [Required]
        public string BranchLocation { get; set; }
        [Required]
        public decimal MedicalServicePrice { get; set; }
        public int? AppointmentDateID { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class UplodedFile
    {
        public int ID { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
 

        [ForeignKey("BranchGusetService")]
        public int BGSID { get; set; }
        public virtual BranchGusetService BranchGusetService { get; set; } 


        
    }
}

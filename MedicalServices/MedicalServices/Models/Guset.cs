using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalServices.Models
{
    public class Guset
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<BranchGusetService> BranchGusetServices { get; set; } = new List<BranchGusetService>();


    }
}

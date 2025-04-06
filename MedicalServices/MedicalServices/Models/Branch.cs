using MedicalServices.Repository;
using MedicalServices.ViewModel;

namespace MedicalServices.Models
{
    public class Branch: InterfaceForSort,InterfaceByID,IHasImage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public string ContactPhone {  get; set; }

        public string?  Image { get; set; }

        #region Collections
        public virtual List<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();
        public virtual List<Admin> Admins { get; set; } = new List<Admin>();
        public virtual List<Assastant> Assastants { get; set; } = new List<Assastant>();
        public virtual List<Accountant> Accountants { get; set; } = new List<Accountant>();
        public virtual List<BranchGusetService> BranchGusetServices { get; set; } = new List<BranchGusetService>();
        public virtual List<Doctor> Doctors { get; set; } = new List<Doctor>(); 
        #endregion


    }
}

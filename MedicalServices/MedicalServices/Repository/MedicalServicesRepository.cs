using MedicalServices.Models;
using MedicalServices.ViewModel;

namespace MedicalServices.Repository
{
    public class MedicalServicesRepository: IMedicalServicesRepository
    {
        private readonly MyContext myContext;

        public MedicalServicesRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public void Update(int id,MedicalService NewMedical)
        {
            MedicalService OldMedicalService=myContext.MedicalServices.FirstOrDefault(X=>X.ID==id);
            if (OldMedicalService != null) {
                OldMedicalService.Name = NewMedical.Name;
                OldMedicalService.Price = NewMedical.Price;
                OldMedicalService.CatigoryID = NewMedical?.CatigoryID ?? 0;
                OldMedicalService.Image = NewMedical.Image;
                OldMedicalService.BranchID = NewMedical?.BranchID ?? 0;
                OldMedicalService.Description = NewMedical.Description;
            }
            myContext.SaveChanges();
        }


    }
}

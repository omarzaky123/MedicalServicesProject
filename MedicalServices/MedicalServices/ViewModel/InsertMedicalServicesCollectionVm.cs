using MedicalServices.Models;

namespace MedicalServices.ViewModel
{
    public class InsertMedicalServicesCollectionVm
    {
        public List<Branch> branches  { get; set; }=new List<Branch>(); 
        public List<Catigory> Catigorys { get; set; }=new List<Catigory>(); 
        public List<MedicalService> MedicalServices { get; set; }=new List<MedicalService>(); 

    }
}

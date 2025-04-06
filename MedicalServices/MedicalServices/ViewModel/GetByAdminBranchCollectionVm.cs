using MedicalServices.Models;

namespace MedicalServices.ViewModel
{
    public class GetByAdminBranchCollectionVm
    {
        public List<MedicalService> RelatedServices { get; set; }
        public List<Catigory> Catigorys { get; set; }

        public List<BranchGusetService> RelatedBGS {  get; set; }   
        public List<Doctor> RelatedDoctors { get; set; }   
        public List<Admin> RelatedAdmins { get; set; }   
        public List<Assastant> RelatedAssastants { get; set; }   
        public List<Accountant> RelatedAccountants { get; set; }

        public decimal TotalGain { get; set; }
        public decimal TodayGain { get; set; }
        public decimal TotalSalaryForEmployees { get; set; }
        public decimal TotalGainThisMonth { get; set; }
        public decimal NetTotalGainThisMonth { get; set; }
    }
}

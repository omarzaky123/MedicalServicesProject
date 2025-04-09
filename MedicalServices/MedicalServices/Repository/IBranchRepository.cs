using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public interface IBranchRepository
    {
         List<MedicalService> GetRelatedMedical(int id);   

         List<DateBranch> GetRelatedAvilableDates(int branchid);

        List<Catigory> GetRelateCatigorys(int branchid);

        List<BranchGusetService> GetRelatedBGS(int id);
        List<Assastant> GetRelatedAssastant(int id);
        List<Accountant> GetRelatedAccounts(int id);
        List<Doctor> GetRelatedDoctors(int id);
        List<Admin> GetRelatedAdmins(int id);

        string BranchName(int id);
        decimal TotalGain(int id);
        decimal TodayGain(int id);
        decimal TotalSalaryForEmployee(int id);
        decimal TotalGainForMonth(int id,int start=0);

        decimal NetTotalGainForMonth(int id, int start = 0);

        List<(string serviceName, int Count)> MostRequiredService(int id);

        void Delete(int id);

        void Update(int id, Branch newBranch);
    }
}

using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class BranchRepository : IBranchRepository
    {
        MyContext context;
        private readonly IGeneralRepository<BranchGusetService> generalRepositoryBGS;

        #region Ctor
        public BranchRepository(MyContext _context,IGeneralRepository<BranchGusetService> generalRepositoryBGS)
        {
            context = _context;
            this.generalRepositoryBGS = generalRepositoryBGS;
        }

  


        #endregion

        public List<MedicalService> GetRelatedMedical(int id)
        {
            return context.MedicalServices.Where(M => M.BranchID == id).ToList();    
        }
        public List<Accountant> GetRelatedAccountants(int id)
        {
            return context.Accountants.Where(M => M.BranchID == id).ToList();
        }
        public List<DateBranch> GetRelatedAvilableDates(int branchid)
        {
            List<DateBranch> dateBranches = context.DateBranchs
                .Where(Db => (Db.BranchId == branchid) && (Db.State == true)).ToList();
            List<DateBranch> dateBranchNonExpire= new List<DateBranch>();
            foreach (var dateBranch in dateBranches) {
                if (dateBranch.Date.AppoinmentDate.Date > DateTime.Now.Date)
                {
                    dateBranchNonExpire.Add(dateBranch);
                }
                else if(dateBranch.Date.AppoinmentDate.Date == DateTime.Now.Date)
                {
                    if (dateBranch.Date.AppoinmentDate.Hour > DateTime.Now.Hour)
                    {
                        dateBranchNonExpire.Add(dateBranch);
                    }
                    else if(dateBranch.Date.AppoinmentDate.Hour == DateTime.Now.Hour)
                    {
                        if (dateBranch.Date.AppoinmentDate.Minute >= DateTime.Now.Minute)
                        {
                            dateBranchNonExpire.Add(dateBranch);
                        }
                    }
                   
                }

            }
            return dateBranchNonExpire;
        }

        public List<Catigory> GetRelateCatigorys(int branchid)
        {
            return context.Catigorys.FromSqlInterpolated($"EXEC GetBranchCategories @BranchID = {branchid};").ToList();
        }
        public List<BranchGusetService> GetRelatedBGS(int id)
        {
            List<BranchGusetService> branchGusetServices = context.BranchGusetServices
                .Where(BGS=>BGS.BranchID==id).ToList();
                return branchGusetServices; 
        }
        public List<Doctor> GetRelatedDoctors(int id) {
        
            return context.Doctors.Where(D=>D.BranchID==id).ToList();
        }
        public List<Assastant> GetRelatedAssastant(int id)
        {

            return context.Assastants.Where(D => D.BranchID == id).ToList();
        }
        public List<Accountant> GetRelatedAccounts(int id)
        {

            return context.Accountants.Where(D => D.BranchID == id).ToList();
        }
        public List<Admin> GetRelatedAdmins(int id)
        {
            return context.Admins.Where(D => D.BranchID == id).ToList();
        }
        public string BranchName(int id)
        {
            string name = context.Branchs.FirstOrDefault(B=>B.ID==id)?.Name;
            return name;
        }
        public decimal TotalGain(int id)
        {
            List<BranchGusetService> orders 
                = context.BranchGusetServices.Where(B => B.BranchID == id).ToList();
            decimal totalgain=orders.Select(X=>X?.MedicalService?.Price??0).Sum();
            return totalgain;
        }
        public decimal TodayGain(int id)
        {
            return context.BranchGusetServices
                .Where(B => B.BranchID == id)
                .Where(B => B.DateBranch.Date.AppoinmentDate.Date == DateTime.Today)
                .Sum(X => X.MedicalService != null ? X.MedicalService.Price : 0);
        }
        public decimal TotalSalaryForEmployee(int id)
        {
            List<Admin> admins=GetRelatedAdmins(id);
            List<Assastant> assastants=GetRelatedAssastant(id);
            List<Accountant> accountants=GetRelatedAccountants(id);
            List<Doctor> doctors=GetRelatedDoctors(id);
            List<IWorker> mergedList = doctors.Cast<IWorker>()
                     .Concat(assastants.Cast<IWorker>())
                     .Concat(accountants.Cast<IWorker>())
                     .Concat(admins.Cast<IWorker>())
                     .ToList();
            return mergedList.Sum(S => S?.User?.Salary??0);
        }
        public decimal TotalGainForMonth(int id,int start=0)
        {
            var today = DateTime.Today.AddMonths(start);
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            return context.BranchGusetServices
                .Where(B => B.BranchID == id)
                .Where(B => B.DateBranch.Date.AppoinmentDate.Date >= firstDayOfMonth &&
                            B.DateBranch.Date.AppoinmentDate.Date <= lastDayOfMonth)
                .Sum(X => X.MedicalService != null ? X.MedicalService.Price : 0);
        }
        public decimal NetTotalGainForMonth(int id,int start=0)
        {
            decimal totalgainformonth = TotalGainForMonth(id, start);
            decimal totalsalars = TotalSalaryForEmployee(id);
            return totalgainformonth - totalsalars; 
        }

        public List<(string serviceName, int Count)> MostRequiredService(int id)
        {
            return context.BranchGusetServices
                .Where(X => X.BranchID == id)
                .GroupBy(B => B.MedicalService)
                .Select(X => new
                {
                    serviceName = X.Key.Name,
                    count = X.Count()
                })
                .OrderByDescending(X => X.count)
                .ToList()
                .Select(X=>(X.serviceName,X.count))
                .ToList();
        }

        public void Update(int id, Branch newBranch)
        {
            Branch oldBranch = context.Branchs.FirstOrDefault(x => x.ID == id);
            if (oldBranch != null)
            {
                oldBranch.Name = newBranch.Name;
                oldBranch.ContactPhone = newBranch.ContactPhone;
                oldBranch.Image = newBranch.Image;
                oldBranch.Location = newBranch.Location;
            }
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            Branch branch = context.Branchs.FirstOrDefault(X => X.ID == id);
            if (branch != null)
            {
                List<MedicalService> services = GetRelatedMedical(id);
                List<BranchGusetService> BGS = GetRelatedBGS(id);
                List<Admin> admins = GetRelatedAdmins(id);
                List<Assastant> Assastants = GetRelatedAssastant(id);
                List<Accountant> Accountants = GetRelatedAccounts(id);
                List<Doctor> Doctors = GetRelatedDoctors(id);
                foreach (var service in services)
                {
                    context.Remove(service);
                    context.SaveChanges();
                }
                foreach (var order in BGS)
                {
                    order.Branch = null;
                }
                foreach (var admin in admins)
                {
                    admin.Branch = null;
                }
                foreach (var doctor in Doctors)
                {
                    doctor.Branch = null;
                }
                foreach (var assastant in Assastants)
                {
                    assastant.Branch = null;
                }
                foreach (var Accountant in Accountants)
                {
                    Accountant.Branch = null;
                }
                context.Branchs.Remove(branch);
                context.SaveChanges();

            }


        }
    }
}

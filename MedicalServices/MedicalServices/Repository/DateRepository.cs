using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class DateRepository: IDateRepository
    {
        private readonly MyContext myContext;

        public DateRepository(MyContext _myContext)
        {
            myContext = _myContext;
        }


        public List<Date> UpdateDates()
        {
            FormattableString sqlQuery = $"EXEC [dbo].[UpdateFirstRowWithTodaysDate];";
            List<Date> dates = myContext.Dates.FromSqlInterpolated(sqlQuery).ToList();
            return dates;
        }
        public void DeleteAllDateForBranch()
        {
            List<DateBranch> dateBranches = myContext.DateBranchs.ToList();
            myContext.DateBranchs.RemoveRange(dateBranches);
            myContext.SaveChanges();
        }
        public void AddDateForBranch(int branchid,int dateid) {
            DateBranch dateBranch = new DateBranch();
            dateBranch.DateId = dateid;
            dateBranch.BranchId = branchid;
            dateBranch.State = true;
            myContext.DateBranchs.Add(dateBranch);
            myContext.SaveChanges();

        }  

        public List<Date> GetDates() {
            return myContext.Dates.ToList();
        }

        public void DeleteDateForBranch(int branchid) { 
        
            List<DateBranch> dateBranches=myContext.DateBranchs.Where(DB=>DB.BranchId==branchid).ToList();
            myContext.DateBranchs.RemoveRange(dateBranches);
            myContext.SaveChanges();
        }

        public string DateName(int dateid) { 
            string date =  myContext.Dates.FirstOrDefault(D=>D.ID==dateid)?.AppoinmentDate.ToString();
            return date;

        }






    }
}

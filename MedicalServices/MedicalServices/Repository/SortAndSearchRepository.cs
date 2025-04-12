using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class SortAndSearchRepository<T> : ISortAndSearchRepository<T>
        where T : class, IBaseInterface
    {
        private MyContext context;
        private readonly DbSet<T> dbSet;

        public SortAndSearchRepository(MyContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public List<T> SortByName(int branchid=0)
        {
            if(branchid==0)
                return dbSet.OrderBy(c => ((InterfaceForSort)(object)c).Name).ToList();
            else
                return dbSet.OrderBy(c => ((InterfaceForSort)(object)c).Name).Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();
        }
        public List<T> SortByNameDescending(int branchid = 0)
        {
            if (branchid == 0)
                return dbSet.OrderByDescending(c => ((InterfaceForSort)(object)c).Name).ToList();
            else
                return dbSet.OrderByDescending(c => ((InterfaceForSort)(object)c).Name).Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();
        }
        public List<T> SortById(int branchid = 0)
        {
            if (branchid == 0)
                return dbSet.OrderBy(c => ((InterfaceByID)(object)c).ID).ToList();
            else
                return dbSet.OrderBy(c => ((InterfaceByID)(object)c).ID).Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();
        }
        public List<T> SortByIdDescending(int branchid = 0)
        {
            if (branchid == 0)
                return dbSet.OrderByDescending(c => ((InterfaceByID)(object)c).ID).ToList();
            else
                return dbSet.OrderByDescending(c => ((InterfaceByID)(object)c).ID).Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();

        }

        public List<T> SortWorkerByName()
        {
            return dbSet
                .AsEnumerable() 
                .OrderBy(x => ((IWorker)(object)x).User.UserName) 
                .ToList();
        }
        public List<T> SortWorkerByNameDes()
        {
            return dbSet
                .AsEnumerable() 
                .OrderByDescending(x => ((IWorker)(object)x).User.UserName) 
                .ToList();
        }
        public List<T> SortWorkerBySalary()
        {
            return dbSet
                .AsEnumerable() 
                .OrderByDescending(x => ((IWorker)(object)x).User.Salary) 
                .ToList();
        }
        public List<T> SortWorkerByAge()
        {
            return dbSet
                .AsEnumerable() // Fetch data from the database
                .OrderByDescending(x => ((IWorker)(object)x).User.Age) // Sort in memory
                .ToList();
        }

        public List<T> SortByDate(int branchid=0)
        {

            if (branchid == 0)
            {
                return dbSet
                    .AsEnumerable()
                    .GroupBy(x => ((InterfaceForSortBranchId)(object)x)?.BranchID)
                    .SelectMany(group => group
                        .Where(x => ((InterfaceByDate)(object)x)?.DateBranch?.Date?.AppoinmentDate != null)
                        .OrderBy(x => ((InterfaceByDate)(object)x).DateBranch.Date.AppoinmentDate)
                    )
                    .ToList();
            }
            else
            {
                var orderlist = dbSet.AsEnumerable()
                    .Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid)
                    .GroupBy(x => ((InterfaceForSortBranchId)(object)x)?.BranchID)
                    .SelectMany(group => group
                        .Where(x => ((InterfaceByDate)(object)x)?.DateBranch?.Date?.AppoinmentDate != null)
                        .OrderBy(x => ((InterfaceByDate)(object)x).DateBranch.Date.AppoinmentDate)
                    )
                    .ToList();
                return orderlist;
            }
        }

        public List<T> SearchByName(string name,int branchid=0)
        {
            if (branchid == 0)
            {
                return dbSet
                    .Where(c => ((InterfaceForSort)(object)c).Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }
            else
            {
                return dbSet
                    .Where(c => ((InterfaceForSort)(object)c).Name.ToLower().Contains(name.ToLower()))
                    .Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();
            }
        }

        public List<T> SearchByGuset(string name,int branchid=0)
        {
            if (branchid == 0)
            {
                return dbSet
                    .AsEnumerable()
                    .Where(x => ((InterfaceForBGS)(object)x).Guset.User.UserName
                        .ToLower().Contains(name.ToLower()))
                    .ToList();
            }
            else {
                return dbSet
                    .AsEnumerable()
                    .Where(x => ((InterfaceForBGS)(object)x).Guset.User.UserName
                    .ToLower().Contains(name.ToLower()))
                    .Where(c => ((InterfaceForSortBranchId)(object)c).BranchID == branchid).ToList();
            }
        }

        public List<T> SearchWorkerByName(string name)
        {
            return dbSet
                .AsEnumerable() 
                .Where(x => ((IWorker)(object)x).User.UserName.ToLower().Contains(name.ToLower())) 
                .ToList();
        }

        public List<T> StateSort(string Sort,int branchid=0)
        {
            switch (Sort)
            {
                case "Name":
                    return SortByName(branchid);
                case "NameDes":
                    return SortByNameDescending(branchid);
                case "Id":
                    return SortById(branchid);
                case "IdDes":
                    return SortByIdDescending(branchid);
                case "NameWorker":
                    return SortWorkerByName();
                case "NameWorkerDes":
                    return SortWorkerByNameDes();
                case "Salary":
                    return SortWorkerBySalary();
                case "Age":
                    return SortWorkerByAge() ;
                case "Date":
                    return SortByDate(branchid);
                default:
                    return SortById(branchid);
            }
        }
    }

}

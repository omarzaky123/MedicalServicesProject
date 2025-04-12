using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public class BranchGusetServiceRepository : IBranchGusetServiceRepository
    {
        MyContext context;
        public BranchGusetServiceRepository(MyContext myContext)
        {
            context = myContext;
        }
        public void Insert(BranchGusetService BGS)
        {
            context.BranchGusetServices.Add(BGS);
                context.SaveChanges();
        }
        public void SetEamilSent(int id,bool State=true)
        {
            BranchGusetService branchGusetService = context.BranchGusetServices.FirstOrDefault(X=>X.ID==id);
            if (branchGusetService != null) { 
                branchGusetService.EmailSent = State;
                context.SaveChanges();
            }
        }
        public void SetUploade(int id, bool State = true)
        {
            BranchGusetService branchGusetService = context.BranchGusetServices.FirstOrDefault(X => X.ID == id);
            if (branchGusetService != null)
            {
                branchGusetService.Uploaded = State;
                context.SaveChanges();
            }
        }
        public List<BranchGusetService> GetForCertainBranchNotUplodedOrders(int branchid=0) {

            if (branchid==0)
            {
                return context.BranchGusetServices.Where(X=>X.Uploaded==false)
                    .ToList();
            }
            else
            {
                return context.BranchGusetServices.Where(X => X.BranchID == branchid && X.Uploaded == false)
                     .ToList();
            }
        }


        public void Delete(int branchid,int gusetid)
        {
           BranchGusetService branchGusetService = context.BranchGusetServices
                .Where(BGS=>(BGS.BranchID==branchid) && (BGS.GusetID==gusetid)).FirstOrDefault();
            if (branchGusetService != null)
            {
                context.Remove(branchGusetService);
                context.SaveChanges();  
            }
        }

    }
}

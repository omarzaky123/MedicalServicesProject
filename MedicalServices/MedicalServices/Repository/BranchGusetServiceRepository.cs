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
        //Get Gusets That Make A orders for certaint Branch(Unique)
        public List<Guset> GetGusetsForCertainBranch(int branchid=0)
        {
            if (branchid == 0)
            {
                //all Gusetes
                return context.BranchGusetServices.Select(X => X.Guset).Distinct().ToList();
            }
            else
            {
                return context.BranchGusetServices.Where(BGS=>BGS.BranchID==branchid)
                    .Select(BGS=>BGS.Guset).Distinct().ToList();
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


        public List<MedicalService> RelatedOrdersForCertainBranch(int guestId,int branchid=0) {
                if (branchid == 0)
                {
                    return context.BranchGusetServices.Where(BGS => BGS.GusetID==guestId && BGS.Uploaded==false)
                        .Select(BGS => BGS.MedicalService).ToList();
                }
                else {

                    return context.BranchGusetServices
                        .Where(BGS => BGS.GusetID == guestId && BGS.BranchID==branchid && BGS.Uploaded == false)
                        .Select(BGS => BGS.MedicalService).ToList();
                }
            
        }
        public int GetIdByServicesIdAndGusetNameId(int gusetId,int serviceId) { 
        
                return context.BranchGusetServices.Where(X=>X.MedicalService.ID== serviceId && X.GusetID== gusetId && X.Uploaded==false)
                .Select(BGS=>BGS.ID).FirstOrDefault();
        }

    }
}

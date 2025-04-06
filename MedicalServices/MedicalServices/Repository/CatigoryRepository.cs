using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MedicalServices.Repository
{
    public class CatigoryRepository : ICatigoryRepository
    {
        MyContext context;
        public CatigoryRepository(MyContext _context)
        {
            context = _context;
        }
        public List<MedicalService> GetRelatedServicesBranch(int BranchId,int CatigoryId)
        {
            return context.MedicalServices.Where(M=>M.CatigoryID== CatigoryId && M.BranchID==BranchId).ToList();
        }

        
    }
}

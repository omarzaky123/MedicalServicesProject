using MedicalServices.Models; // Keep only this
using System.Linq; // Ensure LINQ is included if needed



namespace MedicalServices.Repository
{
    public class GusetRepository :IGusetRepository
    {
        MyContext context;
        public GusetRepository(MyContext _context)
        {
            context = _context;
        }
        public Guset AccountId(string id)
        {
            return context.Gusets.FirstOrDefault(G => G.UserID == id);
        }

        public List<BranchGusetService> RelatedBGS(int id)
        {
            List<BranchGusetService> list= context.BranchGusetServices.Where(BGS => BGS.GusetID == id).ToList();
			return context.BranchGusetServices.Where(BGS => BGS.GusetID == id).ToList();
        }
    }
}

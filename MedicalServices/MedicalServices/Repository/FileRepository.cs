using MedicalServices.Models;

namespace MedicalServices.Repository
{
    public class FileRepository: IFileRepository
    {
        private readonly MyContext context;

        public FileRepository(MyContext context)
        {
            this.context = context;
        }


       public List<UplodedFile> GetAll()
        {
            return context.UplodedFiles.ToList(); 
        }
        public List<UplodedFile> GetAllForCertainBranch(int branchId)
        {
            return context.UplodedFiles.Where(X=>X.BranchGusetService.BranchID==branchId).ToList();
        }
        public void InsertRange(List<UplodedFile> uplodedFiles)
        {
            context.UplodedFiles.AddRange(uplodedFiles); 
            context.SaveChanges();
        }
        public UplodedFile GetByStoredName(string storedName) {

            return context.UplodedFiles.FirstOrDefault(X => X.StoredFileName == storedName);
        }
        public void Delete(string storedName)
        {

            UplodedFile file= GetByStoredName(storedName);
            if (file != null) { 
            
              context.UplodedFiles.Remove(file);
                context.SaveChanges();
            }
        }

    }
}

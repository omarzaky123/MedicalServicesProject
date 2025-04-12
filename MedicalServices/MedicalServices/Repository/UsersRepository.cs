using MedicalServices.Models;
using Microsoft.AspNetCore.Identity;

namespace MedicalServices.Repository
{
    public class UsersRepository: IUserRepository
    {
        private readonly MyContext myContext;

        public UsersRepository(MyContext _myContext)
        {
            myContext = _myContext;
            
        }
        public ApplicationUser GetById(string id) { 
        
            return myContext.Users.FirstOrDefault(U=>U.Id==id);
        }
        public void Delete(string userid)
        {
            ApplicationUser user = myContext.Users.FirstOrDefault(U=>U.Id==userid);
            myContext.Users.Remove(user);
            myContext.SaveChanges();
        }
    }
}

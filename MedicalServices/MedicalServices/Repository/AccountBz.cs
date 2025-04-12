using MedicalServices.Models;
using MedicalServices.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace MedicalServices.Repository
{
    public class AccountBz:IAccountBz
    {
        private readonly IGeneralRepository<Doctor> generalDoctorRepository;
        private readonly IGeneralRepository<Assastant> generalAssastantRepository;
        private readonly IGeneralRepository<Accountant> generalAccountRepository;
        private readonly IGeneralRepository<Admin> generalAdminRepository;
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountBz(IGeneralRepository<Doctor> generalDoctorRepository,
            IGeneralRepository<Assastant> generalAssastantRepository,
            IGeneralRepository<Accountant> generalAccountRepository,
            IGeneralRepository<Admin> generalAdminRepository,
            IUserRepository userRepository,
            IWebHostEnvironment webHostEnvironment
            )
        {
            this.generalDoctorRepository = generalDoctorRepository;
            this.generalAssastantRepository = generalAssastantRepository;
            this.generalAccountRepository = generalAccountRepository;
            this.generalAdminRepository = generalAdminRepository;
            this.userRepository = userRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void SetInTheTable(string RoleName, ApplicationUser applcationUser, AssignRoleVm assignRoleVm)
        {
            switch (RoleName)
            {
                case "Doctor":
                    Doctor doctor = new Doctor();
                    doctor.UserID = applcationUser.Id;
                    doctor.BranchID = assignRoleVm.BranchId;
                    generalDoctorRepository.Insert(doctor);
                break;
                case "Assastant":
                    Assastant assastant = new Assastant();
                    assastant.UserID = applcationUser.Id;
                    assastant.BranchID = assignRoleVm.BranchId;
                    generalAssastantRepository.Insert(assastant);
                break;
                case "Accountant":
                    Accountant Accountant = new Accountant();
                    Accountant.UserID = applcationUser.Id;
                    Accountant.BranchID = assignRoleVm.BranchId;
                    generalAccountRepository.Insert(Accountant);
                break;
                case "Admin":
                    Admin Admin = new Admin();
                    Admin.UserID = applcationUser.Id;
                    Admin.BranchID = assignRoleVm.BranchId;
                    generalAdminRepository.Insert(Admin);
                break;
            }
        }

    }
}

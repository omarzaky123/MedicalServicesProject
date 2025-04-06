using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MedicalServices.Models
{
    public class MyContext:IdentityDbContext<ApplicationUser>
    {
        #region Ctor
        public MyContext()
        {


        }
        public MyContext(DbContextOptions options) : base(options) { } 
        #endregion

        #region Dbsets

        public virtual DbSet<Guset> Gusets { get; set; }    
        public virtual DbSet<Branch> Branchs { get; set; }    
        public virtual DbSet<MedicalService> MedicalServices { get; set; }    
        public virtual DbSet<Doctor> Doctors { get; set; }    
        public virtual DbSet<Assastant> Assastants { get; set; }    
        public virtual DbSet<Accountant> Accountants { get; set; }    
        public virtual DbSet<Admin> Admins { get; set; }    
        public virtual DbSet<BranchGusetService> BranchGusetServices { get; set; }
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<DateBranch> DateBranchs { get; set; }
        public virtual DbSet<Catigory> Catigorys { get; set; }
        public virtual DbSet<UplodedFile> UplodedFiles { get; set; }
       
       
        #endregion

        #region OnCreating
        protected override void OnModelCreating(ModelBuilder builder)
        {




            builder.Entity<MedicalService>()
                .HasMany(M => M.BranchGusetServices)
                .WithOne(BGS => BGS.MedicalService)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<MedicalService>()
                .Property(M => M.Price)
                .HasDefaultValue(0);
            builder.Entity<MedicalService>()
                .HasOne(M => M.Catigory)
                .WithMany(C=>C.MedicalServices)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);

            //builder.Entity<Branch>()
            //    .HasMany(B => B.MedicalServices)
            //    .WithOne(M => M.Branch)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Branch>()
            //    .HasMany(B => B.Admins)
            //       .WithOne(M => M.Branch)
            //       .OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<Branch>()
            //    .HasMany(B => B.BranchGusetServices)
            //        .WithOne(M => M.Branch)
            //    .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<BranchGusetService>()
                .HasOne(BGS=>BGS.DateBranch)
                .WithMany(Db=>Db.BranchGusetServices)
                .OnDelete(DeleteBehavior.SetNull);


            builder.Entity<UplodedFile>()
                .Property(u => u.BGSID)
                 .ValueGeneratedNever(); // Forces EF to use the value you provide

        }
        #endregion



    }
}

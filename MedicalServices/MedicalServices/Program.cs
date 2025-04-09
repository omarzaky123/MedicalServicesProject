using MedicalServices.Emali;
using MedicalServices.Models;
using MedicalServices.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Security.Principal;

namespace MedicalServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            #region DepndencyInjuction
            #region ConnetctionString
            string connection = builder.Configuration.GetConnectionString("OutSide");
            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(connection)
                       .UseLazyLoadingProxies(); // Enable lazy loading
            });

            #endregion
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                     //You can ass your constraint here to the pass
                     options => options.Password.RequireDigit = true
                     ).
             AddEntityFrameworkStores<MyContext>();//AddEntityFrameworkStores use sql server as

            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
            builder.Services.AddScoped<IGusetRepository, GusetRepository>();
            builder.Services.AddScoped<IBranchGusetServiceRepository, BranchGusetServiceRepository>();
            builder.Services.AddScoped<ICatigoryRepository, CatigoryRepository>();
            builder.Services.AddScoped<IAccountBz, AccountBz>();
            builder.Services.AddScoped<IBranchGusetServiceBz, BranchGusetServiceBz>();
            builder.Services.AddScoped<IUserRepository, UsersRepository>();
            builder.Services.AddScoped(typeof(ISortAndSearchRepository<>), typeof(SortAndSearchRepository<>));
            builder.Services.AddScoped<IDateRepository, DateRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IMedicalServicesRepository, MedicalServicesRepository>();
            builder.Services.AddScoped(typeof(IWorkerRepository<>), typeof(WorkerRepository<>));
            builder.Services.AddTransient<IMailingService, MailingService>();





            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Welcome}/{action=WelcomePage}/{id?}");
            app.Run();
        }
    }
}

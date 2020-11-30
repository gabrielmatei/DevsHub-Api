using DevsHub.Data;
using DevsHub.Data.Repositories;
using DevsHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevsHub.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            #region Repositories
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserProfilesRepository, UserProfilesRepository>();
            services.AddScoped<IContestsRepository, ContestsRepository>();
            services.AddScoped<ITutorialsRepository, TutorialsRepository>();
            services.AddScoped<ITutorialCategoriesRepository, TutorialCategoriesRepository>();
            services.AddScoped<IAnnouncementsRepository, AnnouncementsRepository>();
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IContestService, ContestService>();
            services.AddTransient<ITutorialService, TutorialService>();
            services.AddTransient<IAnnouncementsService, AnnouncementsService>();
            #endregion
        }
    }
}

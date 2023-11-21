using LogicService.Data;
using LogicService.Profiles;
using LogicService.Services;

namespace backend.Extensions
{ 
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationService(this IServiceCollection services,IConfiguration config)
        {
            services.AddSingleton<DataContexst>();
            services.AddScoped<OrganizationService>();
            services.AddScoped<UserService>();
            services.AddScoped<VerificationService>();
            services.AddScoped<TokenService>();
            services.AddScoped<RequstService>();

            services.AddAutoMapper(typeof(MappingProfile));



            return services;
        }
       
    }
}

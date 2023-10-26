using Example_App.ApplicationApp.Interface_Controller;
using Example_App.ApplicationApp.Service;
using Example_App.Infraestructura.Model;

namespace Example_App.API.Extensions
{
    public static class AplicationServiceExtensions
    {

        public static void AddAplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUser, UserService>();
        }

    }
}

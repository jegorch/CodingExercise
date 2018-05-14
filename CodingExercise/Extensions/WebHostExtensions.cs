using CodingExercise.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CodingExercise.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ApplicationDbContext>();

                DataSeeder.SeedData(context);
            }
            return host;
        }
    }
}

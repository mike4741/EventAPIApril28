using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TokenServiceApi.Data;
using TokenServiceApi.Models;

namespace TokenServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            var scope = host.Services.CreateScope();

            //using (var scope = host.Services.CreateScope())
            //{
            var services = scope.ServiceProvider;

            try
            {

                var context = services.GetRequiredService<ApplicationDbContext>();

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                // var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                IdentityDbInit.Initialize(context, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the AuthorizationServer database.");
            }
            //}

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Configuration
{
    public static class ApplicationContextConfiguration
    {
        public static IServiceCollection ConfigureDevelopmentAppContext(this IServiceCollection collection)
        {
            collection.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseInMemoryDatabase("test_db");
            });

            return collection;
        }

        public static IServiceCollection ConfigureTestingAppContext(this IServiceCollection collection,
            IConfiguration Configuration)
        {
            collection.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("PortDatabase"));
            });

            return collection;
        }
    }
}

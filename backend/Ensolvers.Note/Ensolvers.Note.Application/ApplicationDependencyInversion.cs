using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensolvers.Note.Application
{
    public static class ApplicationDependencyInversion
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ApplicationDependencyInversion).Assembly));

            //services.AddMediatR(cfg =>
            //    //cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly)
            //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            //);

            //return services;
        }
    }
}

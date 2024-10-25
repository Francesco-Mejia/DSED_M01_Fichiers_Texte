using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace M01_DAL_Municipalite_SQLServer
{
    public static class ContextGenerateur
    {
        public static void AjouterMunicipaliteContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MunicipaliteContext>(options => options
                .UseSqlServer(configuration
                    .GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}

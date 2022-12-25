using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public static class ConfigureServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<AppDbContext>();
            services.AddSingleton<EfRepository>();
        }
    }
}

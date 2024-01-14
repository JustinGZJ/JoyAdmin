using System;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JoyAdmin.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            options.CustomizeMultiTenants();
            options.AddDbPool<DefaultDbContext>(DbProvider.Npgsql);
        }, "JoyAdmin.Database.Migrations");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            Scoped.Create((_, scope) =>
            {
                var context = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
                var connectionString = context.Database.GetConnectionString();
                Console.WriteLine($"connectionString:{connectionString}");
                context.Database.Migrate();
            });
        }
    }
}
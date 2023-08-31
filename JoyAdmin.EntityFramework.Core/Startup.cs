using System;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

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

        // if (env.IsDevelopment())
        // {
        //     Scoped.Create((_, scope) =>
        //     {
        //         var context = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
        //         context.Database.Migrate();
        //     });
        // }
    }
}
using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Songify.Minimal;

var services = new ServiceCollection();
services.AddTransient<GetsRoutesValueHelper>();
services.AddSingleton<InMemoryRepository>();
var serviceProvider = services.BuildServiceProvider();



Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.Configure(app =>
    {
        // app.Run(async context =>
        // {
        //     await context.Response.WriteAsync("Hello .Net Core!");
        // });

        // app.Use((context, next) =>
        //     {
        //         return next();
        //     }
        // );
        
        // app.Use((context, next) =>
        // {
        //     var cultureQuery = context.Request.Query["culture"];
        //     if (!string.IsNullOrWhiteSpace(cultureQuery))
        //     {
        //         var culture = new CultureInfo(cultureQuery);
        //
        //         CultureInfo.CurrentCulture = culture;
        //         CultureInfo.CurrentUICulture = culture;
        //     }
        //
        //     return next();
        // });

        // app.UseMiddleware<RequestCultureMiddleware>();

        app.UseRequestCulture();
        app.Run(async context =>
        {
            // COnsider following en culture groups
            // en
            // en_GB
            // en_US
            await context
                .Response
                .WriteAsync($"Hello everyone {CultureInfo.CurrentCulture.DisplayName}, " +
                            $"{CultureInfo.InvariantCulture}, " +
                            $"and UI {CultureInfo.CurrentUICulture.DisplayName}");
        });
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {

            
            // endpoints.MapGet("/", async context =>
            // {
            //     await context.Response.WriteAsJsonAsync(new {Id = 1, Name = "Metallica"});
            // });
            
            endpoints.MapGet("/artists/{id:int}", async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<InMemoryRepository>();
                var routeHelper = scope.ServiceProvider.GetRequiredService<GetsRoutesValueHelper>();
                var id = routeHelper.Get<int>(context, "id");
                var artist = repository.Get(id);
                await context.Response.WriteAsJsonAsync(artist);
                // await context.Response.WriteAsJsonAsync(new {Id = 1, Name = "Metallica"});
            });

            endpoints.MapPost("/artists", async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<InMemoryRepository>();
                var artist = await context.Request.ReadFromJsonAsync<Artist>();
                repository.Add(artist);
                
                // var artist = await context.Request.ReadFromJsonAsync<Artist>();
                await context.Response.WriteAsJsonAsync(artist);
            });
            
            endpoints.MapDelete("/artists/{id:int}", async context =>
            {
                using var scope = serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<InMemoryRepository>();
                var routeHelper = scope.ServiceProvider.GetRequiredService<GetsRoutesValueHelper>();
                var id = routeHelper.Get<int>(context, "id");
                repository.Delete(id);
                await context.Response.WriteAsJsonAsync(new {status = "ok"});
            });
            

        });
    });
}).Build().Run();
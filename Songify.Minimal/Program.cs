using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.Configure(app =>
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsJsonAsync(new {Id = 1, Name = "Metallica"});
            });

            endpoints.MapPost("/", async context =>
            {
                var artist = await context.Request.ReadFromJsonAsync<Artist>();
                await context.Response.WriteAsJsonAsync(artist);
            });

        });
    });
}).Build().Run();

record Artist(int Id, string Name);

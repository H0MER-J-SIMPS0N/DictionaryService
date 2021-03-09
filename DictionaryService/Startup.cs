using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryService.Handlers;
using DictionaryService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DictionaryService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStorageService, StorageService>();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStorageService service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            HandlerFactory hFactory = new HandlerFactory();
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("/", async context => await hFactory.CreateHandler<GetAllKeysHandler>(context, service).HandleAsync()) ;
            routeBuilder.MapGet("/{key}", async context => await hFactory.CreateHandler<GetValueHandler>(context, service).HandleAsync());
            routeBuilder.MapPost("/{key}", async context => await hFactory.CreateHandler<AddValueHandler>(context, service).HandleAsync());
            routeBuilder.MapDelete("/{key}", async context => await hFactory.CreateHandler<DeleteValueHandler>(context, service).HandleAsync());
            app.UseRouter(routeBuilder.Build());
        }

    }
}

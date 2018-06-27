using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using System.Reflection;
using ToDoApi.Models;



namespace ToDoApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ToDoContext>(opt =>        //REGISTER
            //    opt.UseInMemoryDatabase("ToDoList"));        //REGISTER

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //var connectionString = @"server=.;database=ToDoDb;Trusted_Connection=True;";
            var connectionString = "Server=.;Database=ToDoDb;Trusted_Connection=True";

            services.AddDbContext<ToDoContext>(o => o.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, ToDoContext dbContext)
        {

            app.UseStaticFiles();

            dbContext.Database.EnsureCreated();

            //// Enable the Swagger UI middleware and the Swagger generator
            //app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            //{
            //    settings.GeneratorSettings.Version = "v1";
            //    settings.GeneratorSettings.Title = "ToDo API";
            //    settings.GeneratorSettings.Description = "A simple ASP.NET Core web API";
            //
            //    settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
            //
            //    
            //});

            app.UseMvc();

            
        }
    }
}


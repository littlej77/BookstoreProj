using BookstoreProj.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProj
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //HERE   we had to make this 
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            // HERE    tells it we are using controllers and views (the MVC pattern)
            services.AddControllersWithViews();

            // HERE    This is setting up the connection to the database. see the appsettings.json 
            //it calls the context made in our Models folder then we have to make sure to say using project.Models
            services.AddDbContext<BookstoreContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
           });

            services.AddScoped<IBookstoreRepository, EFBookstoreProjectRepository>();
            // add this to be able to use razorpages
            services.AddRazorPages();

            //how to add the session stuff

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // HERE    corresponds to the wwwroot. tells it to use the files in that folder
            //add session here
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //we want to execute the endpoint with the most things passed in first..then work down to nothing
                //this is a simpler way of typing but its still name, pattern, default
                //the {} says "this is gonna be a piece of info passed into me. 

                endpoints.MapControllerRoute("categorypage","{bookCategory}/Page{pagenum}", 
                        new { Controller = "Home", action = "Index" });


                //here they just pass in the PAGE NUMBER
                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pagenum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum=1 });


                //this is if we just get a CATEGORY
                endpoints.MapControllerRoute("category", "{bookCategory}",
                    new{ Controller = "Home", action = "Index", pageNum=1});



                //HERE they pass in nothing.  says to follow controller first, then action , then ID
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}

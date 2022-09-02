using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Roqeeb_Project.Context;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;

namespace Roqeeb_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(option => option.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Roqeeb_Project", Version = "v1" });
            });
            services.AddScoped<IUserStore<User>, UserRepository>();
            services.AddScoped<IUserPasswordStore<User>, UserRepository>();
            services.AddScoped<IUserRoleStore<User>, UserRepository>();
            services.AddScoped<IUserEmailStore<User>, UserRepository>();
            services.AddScoped<IQueryableUserStore<User>, UserRepository>();
            services.AddScoped<IUserPhoneNumberStore<User>, UserRepository>();
            services.AddScoped<IRoleStore<Role>, RoleRepository>();
            services.AddScoped<IRoleValidator<Role>, RoleRepository>();
            services.AddScoped<IRoleClaimStore<Role>, RoleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roqeeb_Project v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

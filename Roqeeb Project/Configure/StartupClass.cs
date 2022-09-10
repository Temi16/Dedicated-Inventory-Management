﻿using System;
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
using Roqeeb_Project.Implementation.Repository;
using Roqeeb_Project.Implementation.Service;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;

namespace Roqeeb_Project.Configure
{
    public static class StartupClass
    {
        public static IServiceCollection MyScoped(this IServiceCollection services)
        {
            services.AddScoped<IUserStore<User>, UserRepository>();
            services.AddScoped<IUserPasswordStore<User>, UserRepository>();
            services.AddScoped<IUserRoleStore<User>, UserRepository>();
            services.AddScoped<IUserEmailStore<User>, UserRepository>();
            services.AddScoped<IQueryableUserStore<User>, UserRepository>();
            services.AddScoped<IUserPhoneNumberStore<User>, UserRepository>();
            services.AddScoped<IRoleStore<Role>, RoleRepository>();
            services.AddScoped<IRoleValidator<Role>, RoleRepository>();
            services.AddScoped<IRoleClaimStore<Role>, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ISectionService, SectionService>();
            return services;
        }
    }
}
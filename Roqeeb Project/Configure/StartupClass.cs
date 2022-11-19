using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Roqeeb_Project.Auth.Service;
using Roqeeb_Project.BackgroundTask;
using Roqeeb_Project.Context;
using Roqeeb_Project.Identity;
using Roqeeb_Project.Implementation.Identity.Repositories;
using Roqeeb_Project.Implementation.Repository;
using Roqeeb_Project.Implementation.Service;
using Roqeeb_Project.Interface.Repository;
using Roqeeb_Project.Interface.Service;
using Roqeeb_Project.SendMail;

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
            //services.AddScoped<IRoleValidator<Role>, RoleRepository>();
            //services.AddScoped<IRoleClaimStore<Role>, RoleRepository>();
            services.AddIdentity<User, Role>().AddDefaultTokenProviders();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductCartRepository, ProductCartRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IAdminCartService, AdminCartService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IAdminCartRepository, AdminCartRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMailMessage, MailMessage>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<ISalesCartRepository, SalesCartRepository>();
            services.AddScoped<ISalesCartService, SalesCartService>();
            services.AddScoped<IProductSalesCartRepository, ProductSalesCartRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddHostedService<ProductReminder>();
            services.AddScoped<ILowProduct, LowProduct>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerCartRepository, CustomerCartRepository>();
            services.AddScoped<ICustomerCartService, CustomerCartService>();
            services.AddScoped<IProductCustomerCartRepository, ProductCustomerCartRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }
    }
}

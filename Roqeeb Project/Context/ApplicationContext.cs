﻿using System;
using Microsoft.EntityFrameworkCore;
using Roqeeb_Project.Entities;
using Roqeeb_Project.Identity;

namespace Roqeeb_Project.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Admin>(b => b.Admin)
                .WithOne(d => d.User)
                .HasForeignKey<Admin>(d => d.UserId);

            modelBuilder.Entity<User>()
                .HasOne<Customer>(b => b.Customer)
                .WithOne(d => d.User)
                .HasForeignKey<Customer>(d => d.UserId);

            modelBuilder.Entity<User>()
               .HasOne<Employee>(b => b.Employee)
               .WithOne(d => d.User)
               .HasForeignKey<Employee>(d => d.UserId);
            string userId = Guid.NewGuid().ToString();
            string adminId = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = userId,
                FirstName = "Roqeeb",
                LastName = "Temidayo",
                Email = "raufroqeeb123@gmail.com",
                Username = "RRT",
                Password = "temi123",
                IsDeleted = false,
            });
            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                Id = adminId,
                UserId = userId,
                FirstName = "Roqeeb",
                LastName = "Temidayo",
                Email = "raufroqeeb123@gmail.com",
                Age = 20,
                IsDeleted = false,
            });
            string roleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleId,
                Name = "Admin",
                IsDeleted = false
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                IsDeleted = false
            });

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<ProductPurchase> ProductPurchases { get; set; }
        public DbSet<ProductSales> ProductsSales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }  
        public DbSet<Store> Stores { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ProductSection> ProductSections { get; set; }
    }
}

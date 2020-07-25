using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data.UserModel;

namespace WebApplication4.Data
{
    public class AppDbContext : IdentityDbContext
    {
        
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPhone> OrderPhones { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<OS> OS { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderPhone>().HasKey(sc => new { sc.OrderId, sc.PhoneId });
            base.OnModelCreating(modelBuilder);

        }
    }
}

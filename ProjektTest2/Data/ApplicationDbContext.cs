using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektTest2.Models;

namespace ProjektTest2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet <Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUsers>()
                .HasOne(m => m.Task)
                .WithOne(i => i.ApplicationUsers)
                .HasForeignKey<Task>(b => b.ApplicationUsers_ID);
                //.WithMany(c => c.Tasks);
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Add Identity related model configuration
        //    base.OnModelCreating(modelBuilder);

        //    // Your fluent modeling here
        //}
    }
}

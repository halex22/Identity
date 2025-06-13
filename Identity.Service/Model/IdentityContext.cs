using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.Model
{
    public class IdentityContext: DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (optionsBuilder.IsConfigured)
        //    {
        //        throw new InvalidOperationException("DbContext is already configured. Do not call OnConfiguring directly.");
        //    }            
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(20);
        }
    }
}

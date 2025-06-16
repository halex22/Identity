using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Api.Model;
using Identity.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity.Service.Model
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Request>().ToTable("requests");
            modelBuilder.Entity<Request>()
                .HasKey(r => r.Id)
                .HasName("request_pk");
            // index on Id
            modelBuilder.Entity<Request>().HasIndex(r => r.Id)
                .HasDatabaseName("request_id_idx");
            // Created At
            modelBuilder.Entity<Request>().Property(r => r.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_time");
            // Executed At
            modelBuilder.Entity<Request>()
                .Property(r => r.ExecutedAt)
                .IsRequired(false)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("execution_time");
            // Success
            modelBuilder.Entity<Request>()
                .Property(r => r.Success)
                .IsRequired(false)
                .HasColumnName("success_status");
            // Error Message
            modelBuilder.Entity<Request>()
                .Property(r => r.ErrorMessage)
                .HasMaxLength(500)
                .IsRequired(false)
                .HasColumnName("error_message");

            modelBuilder.Entity<Request>()
                .HasOne(r => r.User)
                .WithMany(u => u.Requests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(r => r.UserId)
                .HasConstraintName("request_user_id_fk");

            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<Role>().HasKey(r => r.Id);
            modelBuilder.Entity<Role>().Property(r => r.Type).IsRequired().HasMaxLength(25);


            modelBuilder.Entity<UserRole>()
                .ToTable("user_roles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

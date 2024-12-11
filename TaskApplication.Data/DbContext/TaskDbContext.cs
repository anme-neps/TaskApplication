using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskApplication.Data.Entities;
using TaskApplication.Data.Enums;

namespace TaskApplication.Data
{
    public class TaskDbContext : IdentityDbContext<Users>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Apply Identity configurations

            // Seed Roles
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                });

            // Seed Users
            var adminUserId = Guid.NewGuid().ToString();
            var userUserId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<Users>();

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = adminUserId,
                    UserName = "AdminAdmin",
                    NormalizedUserName = "ADMINADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "admin"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new Users
                {
                    Id = userUserId,
                    UserName = "UserUser",
                    NormalizedUserName = "USERUSER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "user"),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            // Assign Roles to Users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = userUserId,
                    RoleId = userRoleId
                });

            // Seed Tasks
            modelBuilder.Entity<Tasks>().HasData(
                new Tasks
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description 1",
                    AssignedUserId = userUserId,
                    Status = Status.New
                },
                new Tasks
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description 2",
                    AssignedUserId = userUserId,
                    Status = Status.New
                });
        }

        public DbSet<Tasks> Tasks { get; set; }
    }
}

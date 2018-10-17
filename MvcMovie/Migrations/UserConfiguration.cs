using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcMovie.Models;

namespace MvcMovie.Migrations
{
    public class UserConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public UserConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var hasher = new PasswordHasher();
            //Create objects
            ApplicationUser user1 = new ApplicationUser { UserName = "user1", PasswordHash = hasher.HashPassword("user1Pass") };
            ApplicationUser user2 = new ApplicationUser { UserName = "user2", PasswordHash = hasher.HashPassword("user2Pass") };
            ApplicationUser admin = new ApplicationUser { UserName = "admin", PasswordHash = hasher.HashPassword("adminPass") };
            ApplicationUser supervisor = new ApplicationUser { UserName = "supervisor", PasswordHash = hasher.HashPassword("supervisorPass") };
           
            ApplicationUser userAdmin = new ApplicationUser { UserName = "userAdmin", PasswordHash = hasher.HashPassword("userAdminPass") };
            ApplicationUser userSupervisor = new ApplicationUser { UserName = "userSupervisor", PasswordHash = hasher.HashPassword("userSupervisorPass") };
            ApplicationUser adminSupervisor = new ApplicationUser { UserName = "adminSupervisor", PasswordHash = hasher.HashPassword("adminSupervisorPass") };

            ApplicationUser userAdminSupervisor = new ApplicationUser { UserName = "userAdminSupervisor", PasswordHash = hasher.HashPassword("userAdminSupervisorPass") };
            
            //userManager.UpdateSecurityStampAsync(user1.Id);

            //Now pass them to the userManager - actually insert them to the database
            userManager.Create(user1, "user1Pass");
            userManager.Create(user2, "user2Pass");
            userManager.Create(admin, "adminPass");
            userManager.Create(supervisor, "supervisorPass");
            
            userManager.Create(userAdmin, "userAdminPass");
            userManager.Create(userSupervisor, "userSupervisorPass");
            userManager.Create(adminSupervisor, "adminSupervisorPass");

            userManager.Create(userAdminSupervisor, "userAdminSupervisorPass");

            //Create the roles
            roleManager.Create(new IdentityRole("User"));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Supervisor"));

            //Bind users and roles
            userManager.AddToRole(user1.Id, "User");
            userManager.AddToRole(user2.Id, "User");
            userManager.AddToRole(admin.Id, "Admin");
            userManager.AddToRole(supervisor.Id, "Supervisor");

            userManager.AddToRoles(userAdmin.Id, "User", "Admin");
            userManager.AddToRoles(userSupervisor.Id, "User", "Supervisor");
            userManager.AddToRoles(adminSupervisor.Id, "Admin", "Supervisor");

            userManager.AddToRoles(userAdminSupervisor.Id, "User", "Admin", "Supervisor");

            
            //context.Users.AddOrUpdate(u => u.UserName, supervisor, user1, admin, userAdmin, user2);



            //manager.Create(userAdminSupervisor, "userAdminSupervisorPass");
            //manager.AddToRole(userAdminSupervisor.Id, "User");
            //manager.AddToRole(userAdminSupervisor.Id, "Admin");
            //manager.AddToRole(userAdminSupervisor.Id, "Supervisor");
            /*context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole
                {
                    Name = "Admin"
                },
                new IdentityRole
                {
                    Name = "Supervisor"
                },
                new IdentityRole
                {
                    Name = "User"
                }
            );*/
            
            
        }

    }
}
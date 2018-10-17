using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcMovie.Models;
using MvcMovie.Models.LocalRoles;

namespace MvcMovie.DataService.Repositories
{
    class ApplicationUserRepository
    {
        //Classes to manage users
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        List<ApplicationUser> users;
        RoleManager<IdentityRole> roleManager;
        IQueryable<IdentityRole> rolesFullIEnum;

        public ApplicationUserRepository()
        {
            context = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            users = userManager.Users.ToList();
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rolesFullIEnum = roleManager.Roles;
        }

        public async Task<bool> Add(string userName, string password, string role)
        {
            var user = new ApplicationUser() { UserName = userName };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, role);
            }

            return result.Succeeded;
        }

        /*public void Edit(Product p)
        {
            context.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }

        public Product FindById(int Id)
        {
            var result = (from r in context.Products where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetProducts() { return context.Products; }
        public void Remove(int Id) { Product p = context.Products.Find(Id); context.Products.Remove(p); context.SaveChanges(); }*/
    }
}

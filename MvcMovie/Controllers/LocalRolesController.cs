using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcMovie.Models;
using MvcMovie.Models.LocalRoles;
using WebGrease.Css.Extensions;

namespace MvcMovie.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class LocalRolesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult AddRoleTest()
        {
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole()
                {
                    Name = "TEST"
                });
            //ApplicationUser userAdminSupervisor = new ApplicationUser { UserName = "userAdmin", PasswordHash = hasher.HashPassword("userAdminPass") };
            /*context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole()
                {
                    Name = "TEST2"
                });
            context.SaveChanges();*/
            context.SaveChanges();
            return Content("TEST ROLE ADDED");
        }


        [HttpPost]
        public ActionResult Index(UsersRolesModel model)
        {
            var searchString = model.AdvancedSearchModel.Keyword;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var users = userManager.Users.ToList();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var rolesFullIEnum = roleManager.Roles;

            //Check if user entered a search string
            if (!String.IsNullOrEmpty(searchString))
            {
                //Keep only those users that match the search string
                users = users.Where(u => u.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            //Determine which roles' checkboxes were checked by the user
            for (int i = 0; i < model.AdvancedSearchModel.RoleHolders.Count; i++)
            {
                var currentRole = model.AdvancedSearchModel.RoleHolders[i];
                //If the user checked a checkbox, then for every user that's left after search string matching,
                //check if he is in current role
                if (currentRole.IsChecked)
                {
                    //The loop is decrementing because if you increment and drop current, the whole list will shift by one
                    //This will cause us to miss one element
                    for (int j = users.Count - 1; j >= 0; j--)
                    {
                        ApplicationUser currentUser = users[j];
                        if (!userManager.IsInRole(currentUser.Id, currentRole.RoleName))
                        {
                            //If not in current role, remove him from the list
                            users.RemoveAt(j);
                        }
                    }
                }
            }

            UsersRolesModel returnModel = new UsersRolesModel()
            {
                Roles = rolesFullIEnum,
                Users = users,
                AdvancedSearchModel = new AdvancedSearchModel(rolesFullIEnum.Count())
            };
            return View(returnModel);
        }

        //First request goes here.
        [HttpGet]
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var users = userManager.Users;
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var rolesFullIEnum = roleManager.Roles;

            UsersRolesModel returnModel = new UsersRolesModel()
            {
                Roles = rolesFullIEnum,
                Users = users,
                AdvancedSearchModel = new AdvancedSearchModel(rolesFullIEnum.Count())
            };
            return View(returnModel);
        }

        //Accepts AJAX requests to add/delete a user from a particular role
        [HttpPost]
        public ActionResult UpdateUserRole(AjaxRoleChangeModel model)
        {
            if (ModelState.IsValid)
            {
                var identityStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(identityStore);

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var user = userManager.FindByName(model.UserName);
                if (model.IsChecked)
                {
                    userManager.AddToRole(user.Id, model.RoleName);
                }
                else
                {
                    userManager.RemoveFromRole(user.Id, model.RoleName);
                }

                return Content("ok");
            }
            else
            {
                return Content("not_ok");
            }


        }






        //[AllowAnonymous]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
            //var model = "ok"; // do whatever you need to get your model
            //return Content("ok");
            //return PartialView(model);
        }


        //[AllowAnonymous]
        public async Task<ActionResult> Create(RegisterUserWithRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() {UserName = model.UserName};
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, model.Role);
                    return RedirectToAction("Index", "LocalRoles");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!roleManager.RoleExists(model.Role))
                {
                    roleManager.Create(new IdentityRole(model.Role));
                    return RedirectToAction("Index", "LocalRoles");
                }

            }
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcMovie.Models;
using MvcMovie.Models.LocalRoles;

namespace MvcMovie.Common.Helpers
{
    public class ReturnModelHelper
    {
        public ApplicationDbContext context;

        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;

        public ReturnModelHelper()
        {
            context = new ApplicationDbContext();

            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context)
                );

            
            roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context)
            );
        }

        


        public UsersRolesModel prepareModelForGet()
        {
            return createBasicModel();
        }

        //public UsersRolesModel prepareModelForPost(UsersRolesModel model)
        //{

        //}

        private UsersRolesModel createBasicModel()
        {
            var rolesFullIEnum = roleManager.Roles;
            var users = userManager.Users;
            UsersRolesModel returnModel = new UsersRolesModel() { Roles = rolesFullIEnum, Users = users, AdvancedSearchModel = new AdvancedSearchModel(rolesFullIEnum.Count()) };
            return returnModel;
        }


    }
}
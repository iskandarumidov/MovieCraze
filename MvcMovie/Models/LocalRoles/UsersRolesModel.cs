using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcMovie.Models.LocalRoles
{
    public class UsersRolesModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public AdvancedSearchModel AdvancedSearchModel { get; set; }
    }
}
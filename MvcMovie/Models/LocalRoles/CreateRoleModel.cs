using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcMovie.Models.LocalRoles
{
    public class CreateRoleModel
    {
        [Required]
        public string Role { get; set; }
        //public List<IdentityRole> RolesList { get; set; }
    }
}
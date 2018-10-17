using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.LocalRoles
{
    public class RegisterUserWithRoleModel : RegisterViewModel
    {
        [Required]
        public string Role { get; set; }
    }
}
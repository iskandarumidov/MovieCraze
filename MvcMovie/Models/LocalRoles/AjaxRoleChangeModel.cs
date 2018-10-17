using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    //Model for the incoming AJAX request to alter a user's role
    public class AjaxRoleChangeModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RoleName { get; set; }
        //Whether we want user in this role or not
        [Required]
        public bool IsChecked { get; set; }
    }
}
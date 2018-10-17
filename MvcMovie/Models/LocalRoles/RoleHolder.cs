using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class RoleHolder                     //Holds which roles the user checked in the advanced search
    {
        public RoleHolder()
        {
        }

        public string RoleName { get; set; }
        public bool IsChecked { get; set; }
    }
}
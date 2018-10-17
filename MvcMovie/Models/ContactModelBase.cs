using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class ContactModelBase
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        //[Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [PasswordOptionalConditionsMet]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Passwords do not match")]
        [RegularExpression(@"^(.{8,})", ErrorMessage = "Shorter than 8 characters")]
        public string Password2 { get; set; }

        //[Required(ErrorMessage = "Required")]
        public bool Check18 { get; set; }
    }
}
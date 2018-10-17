using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcMovie.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int UserIdNumeric { get; set; }
        public virtual ICollection<ContactModel> ContactModels { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<ContactModel> ContactModels { get; set; }

        //public System.Data.Entity.DbSet<MvcMovie.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<MvcMovie.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<MvcMovie.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
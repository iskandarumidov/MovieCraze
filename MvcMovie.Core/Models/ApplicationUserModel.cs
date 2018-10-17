using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MvcMovie.Core.Models
{
    class ApplicationUserModel
    {
        public virtual ICollection<ContactModel> ContactModels { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Models;

namespace MvcMovie.Core.Entities
{
    class ApplicationUserEntity : EntityBase
    {
        public virtual ICollection<ContactModel> ContactModels { get; set; }
    }
}

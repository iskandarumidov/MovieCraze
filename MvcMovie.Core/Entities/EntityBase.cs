using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.Core.Entities
{
    public class EntityBase
    {
        public DateTime UpdateDateTime { get; set; }
        public bool Active { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class AdvancedSearchModel
    {
        public AdvancedSearchModel()
        {
            RoleHolders = new List<RoleHolder>();
        }

        public AdvancedSearchModel(int numRoles)
        {
            RoleHolders = new List<RoleHolder>();
            for (int i = 0; i < numRoles; i++)
            {
                RoleHolders.Add(new RoleHolder());
            }
        }

        public string Keyword { get; set; }
        public List<RoleHolder> RoleHolders { get; set; }
    }
}
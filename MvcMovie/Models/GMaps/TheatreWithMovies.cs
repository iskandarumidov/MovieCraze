using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewtonJsonTest.Models.GMaps
    {
    public class TheatreWithMovies
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public HashSet<Movie> Movies { get; set; }

            public override bool Equals(object obj)
            {
                Theatre q = obj as Theatre;
                return q != null && q.Id == this.Id && q.Name == this.Name;
            }

            public override int GetHashCode()
            {
                return this.Id.GetHashCode() ^ this.Name.GetHashCode();
            }
        }
    }
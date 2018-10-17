using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieJsonModel
    {
        public string Title { get; set; }
        public int TmdbId { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
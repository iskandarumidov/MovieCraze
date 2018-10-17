using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MvcMovie.Models
{
    public class Genre
    {
        [ScriptIgnore]
        public int Id { get; set; }

        public int TmdbGenreId { get; set; }
        public string Name { get; set; }

        [ScriptIgnore]
        public List<Movie> Movies { get; set; }
    }
}
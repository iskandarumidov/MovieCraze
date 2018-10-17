using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieGenre
    {
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }

        [Key, Column(Order = 1)]
        public int MovieId { get; set; }

        [Key, Column(Order = 2)]
        public int GenreId { get; set; }
    }
}
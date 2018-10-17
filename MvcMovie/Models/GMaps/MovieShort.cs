using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.GMaps
{
    public class MovieShort
    {
        public MovieShort(string name, string dateTime)
        {
            this.Name = name;
            this.DateTime = dateTime;
        }
        public string Name { get; set; }
        public string DateTime { get; set; }
    }
}
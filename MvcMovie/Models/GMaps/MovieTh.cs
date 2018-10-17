using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMovie.Models.GMaps;

namespace MvcMovie.Models
{

    //using NewtonJsonTest.Models.GMaps;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using JIgnore = Newtonsoft.Json.JsonIgnoreAttribute;

    public class MovieTh
    {
        //[JIgnore]
        //public string TmsId { get; set; }
        //[JIgnore]
        //public string RootId { get; set; }
        [J("subType")]
        public string SubType { get; set; }
        [J("title")]
        public string Title { get; set; }
        [J("releaseYear")]
        public long ReleaseYear { get; set; }
        //[JIgnore]
        //public string ReleaseDate { get; set; }
        //[JIgnore]
        //public string TitleLang { get; set; }
        //[JIgnore]
        //public string DescriptionLang { get; set; }
        //[JIgnore]
        //public string EntityType { get; set; }
        [J("genres")]
        public List<string> Genres { get; set; }
        [J("longDescription")]
        public string LongDescription { get; set; }
        [J("shortDescription")]
        public string ShortDescription { get; set; }
        [J("topCast")]
        public List<string> TopCast { get; set; }
        [J("directors")]
        public List<string> Directors { get; set; }
        [J("officialUrl")]
        public string OfficialUrl { get; set; }
        //[JIgnore]
        //public List<Rating> Ratings { get; set; }
        //[JIgnore]
        //public List<string> Advisories { get; set; }
        [J("runTime")]
        public string RunTime { get; set; }
        [J("preferredImage")]
        public PreferredImage PreferredImage { get; set; }
        [J("showtimes")]
        public List<Showtime> Showtimes { get; set; }
        [J("qualityRating")]
        public QualityRating QualityRating { get; set; }
        [J("audience")]
        public string Audience { get; set; }
        [J("animation")]
        public string Animation { get; set; }
    }

    public class PreferredImage
    {
        [J("width")]
        public string Width { get; set; }
        [J("height")]
        public string Height { get; set; }
        [J("uri")]
        public string Uri { get; set; }
        [J("category")]
        public string Category { get; set; }
        [J("text")]
        public string Text { get; set; }
        [J("primary")]
        public string Primary { get; set; }
        [J("caption")]
        public Caption Caption { get; set; }
    }

    public class Caption
    {
        [J("content")]
        public string Content { get; set; }
        [J("lang")]
        public string Lang { get; set; }
    }

    public class QualityRating
    {
        [J("ratingsBody")]
        public string RatingsBody { get; set; }
        [J("value")]
        public string Value { get; set; }
    }

    public class Rating
    {
        [J("body")]
        public string Body { get; set; }
        [J("code")]
        public string Code { get; set; }
    }

    public class Showtime
    {
        [J("theatre")]
        public Theatre Theatre { get; set; }
        [J("dateTime")]
        public string DateTime { get; set; }
        //[JIgnore]
        //public string Quals { get; set; }
        //[JIgnore]
        //public bool Barg { get; set; }
        [J("ticketURI")]
        public string TicketUri { get; set; }
    }

    public class Theatre
    {
        [J("id")]
        public string Id { get; set; }
        [J("name")]
        public string Name { get; set; }
        //[JIgnore]
        public string Latitude { get; set; }
        //[JIgnore]
        public string Longitude { get; set; }
        //[JIgnore]
        //public HashSet<MovieShort> Movies { get; set; }
        public HashSet<MovieShort> Movies = new HashSet<MovieShort>();

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
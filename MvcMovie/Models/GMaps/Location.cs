namespace GMaps
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using JIgnore = Newtonsoft.Json.JsonIgnoreAttribute;

    public class Location
    {
        //[J("html_attributions")]
        //public object[] HtmlAttributions { get; set; }
        [J("results")]
        public Result[] Results { get; set; }
        [J("status")]
        public string Status { get; set; }
    }

    public class Result
    {
        [J("geometry")]
        public Geometry Geometry { get; set; }
        //[J("icon")]
        //public string Icon { get; set; }
        //[J("id")]
        //public string Id { get; set; }
        [J("name")]
        public string Name { get; set; }
        //[J("photos")]
        //public Photo[] Photos { get; set; }
        //[J("place_id")]
        //public string PlaceId { get; set; }
        [J("rating")]
        public double Rating { get; set; }
        //[J("reference")]
        //public string Reference { get; set; }
        //[J("scope")]
        //public string Scope { get; set; }
        //[J("types")]
        //public string[] Types { get; set; }
        //[J("vicinity")]
        //public string Vicinity { get; set; }
    }

    public class Geometry
    {
        [J("location")]
        public PurpleLocation Location { get; set; }
        //[J("viewport")]
        //public Viewport Viewport { get; set; }
    }

    public class PurpleLocation
    {
        [J("lat")]
        public string Lat { get; set; }
        [J("lng")]
        public string Lon { get; set; }
    }

    //public class Viewport
    //{
    //    [J("northeast")]
    //    public PurpleLocation Northeast { get; set; }
    //    [J("southwest")]
    //    public PurpleLocation Southwest { get; set; }
    //}

    //public class Photo
    //{
    //    [J("height")]
    //    public long Height { get; set; }
    //    [J("html_attributions")]
    //    public string[] HtmlAttributions { get; set; }
    //    [J("photo_reference")]
    //    public string PhotoReference { get; set; }
    //    [J("width")]
    //    public long Width { get; set; }
    //}
}

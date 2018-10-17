using GMaps;
//using NewtonJsonTest.Models;
//using NewtonJsonTest.Models.GMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MvcMovie.Models;
using MvcMovie.Models.GMaps;

namespace NewtonJsonTest.Helpers
{
    public class TheaterGetter
    {
        //public const string TMS_API_KEY = "2upvvq3f97xxxwws7swfmnaz";
        public const string TMS_API_KEY = "98dqh9dsfd5vfc8a7zvhpsha";
        
        private const string TMS_API_KEY_SET_PARAM = "api_key=" + TMS_API_KEY;

        //public const string GOOGLE_MAPS_KEY = "AIzaSyAK_tM6o9a7zmfpswuWukhdkvjSZJY2Mi8";    //iskander.umidov.iu
        public const string GOOGLE_MAPS_KEY = "AIzaSyCfNE6xUrum798R3JeKeA4l578h5N9A2IQ";        //Plur2
        private const string GOOGLE_MAPS_KEY_SET_PARAM = "key=" + GOOGLE_MAPS_KEY;
        public const string GOOGLE_MAPS_RADIUS_PARAM = "radius=";
        public const string GOOGLE_MAPS_LOCATION_PARAM = "location=";
        public const string GOOGLE_MAPS_TYPE_PARAM = "type=";
        public const string GOOGLE_MAPS_KEYWORD_PARAM = "keyword=";

        //private const string radius = "50000";
        private const string TMS_API_RADIUS_PARAM = "radius=";
        private const string TMS_API_UNITS_PARAM = "units=";
        private const string TMS_API_LATITUDE_PARAM = "lat=";
        private const string TMS_API_LONGITUDE_PARAM = "lng=";
        private const string TMS_API_DATE_PARAM = "startDate=";

        /// <summary>
        /// TEST VALUES NEXT. WIll get the actual one from the client's browser
        /// </summary>
        //string latitude = "44.984957";
        //string longitude = "-93.255552";
        //"&zip=55415"
        WebClient wc = new WebClient();

        private HashSet<Theatre> getTheatersWithMovies(string date, string lat, string lon, string radius, string units)
        {
            string requestString = String.Format(
                "http://data.tmsapi.com/v1.1/movies/showings?" + TMS_API_DATE_PARAM + "{0}" + "&" +
                TMS_API_RADIUS_PARAM + "{1}" + "&" + TMS_API_UNITS_PARAM + "{2}" + "&" + TMS_API_LATITUDE_PARAM +
                "{3}" + "&" + TMS_API_LONGITUDE_PARAM + "{4}" + "&" + TMS_API_KEY_SET_PARAM, date, radius, units, lat,
                lon);
            string responseString = wc.DownloadString(requestString);
            
            //string responseString = System.IO.File.ReadAllText(@"C:\Users\isk\Desktop\minn_response_json.txt");


            var gmapsMovies = JsonConvert.DeserializeObject<List<MovieTh>>(responseString);

            HashSet<Theatre> uniqueTheatersSet = new HashSet<Theatre>();
            foreach (MovieTh currentMovie in gmapsMovies)
            {
                foreach (Showtime currentShowtime in currentMovie.Showtimes)
                {
                    uniqueTheatersSet.Add(currentShowtime.Theatre);

                    Theatre myTh = uniqueTheatersSet.First(x => x.Equals(currentShowtime.Theatre));
                    myTh.Movies.Add(new MovieShort(currentMovie.Title, currentShowtime.DateTime));
                    //add to the theater current movie from upper loop and current time of the movie from currentshowtime.datetime
                }
            }
            return uniqueTheatersSet;
        }

        private HashSet<Theatre> getTheaterCoordinates(HashSet<Theatre> uniqueTheatersSet, string lat, string lon, string radius){
            List<Theatre> theatersToRemove = new List<Theatre>();
            foreach (Theatre currentTh in uniqueTheatersSet)
            {
                //"https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyCxwC-hLhTBNUKoeQeHDuiFSvqIueBshAs"
                //string requestString = String.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?" + GOOGLE_MAPS_TYPE_PARAM + "{0}" + "&" + GOOGLE_MAPS_RADIUS_PARAM + "{1}" + "&" + GOOGLE_MAPS_LOCATION_PARAM + "{2}" + "&" + GOOGLE_MAPS_KEY_SET_PARAM + "&" + GOOGLE_MAPS_KEYWORD_PARAM + "{3}", "movie_theater", radius, (lat + "," + lon), System.Web.HttpUtility.UrlEncode(currentTh.Name));
                string requestString = String.Format("https://maps.googleapis.com/maps/api/geocode/json?" + "address=" + currentTh.Name  + "&" + GOOGLE_MAPS_KEY_SET_PARAM);
                string responseString = wc.DownloadString(requestString);
                //JObject respJson = JObject.Parse(responseString);
                //JArray geometry = JArray.Parse(respJson["results"].ToString());
                //Console.WriteLine(geometry);
                var currentLocation = JsonConvert.DeserializeObject<Location>(responseString, new StringNumericConverter());
                //Geometry geom = GetFirstInstance<Geometry>();

                if (currentLocation.Status == "OK")
                {
                    currentTh.Latitude = currentLocation.Results[0].Geometry.Location.Lat;
                    currentTh.Longitude = currentLocation.Results[0].Geometry.Location.Lon;
                }
                else
                {//TODO: zero res, invalid req, etc case select
                    //throw new Exception("Google Maps API Problem!");
                    theatersToRemove.Add(currentTh);
                }                    
            }

            foreach (var currentThToRemove in theatersToRemove)
            {
                uniqueTheatersSet.Remove(currentThToRemove);
            }

            return uniqueTheatersSet;
        }

        public HashSet<Theatre> getTheaters(string date, string lat, string lon, string radius, string units)
        {
            HashSet<Theatre> uniqueTheatersSet = this.getTheatersWithMovies(date, lat, lon, radius, units);
            return this.getTheaterCoordinates(uniqueTheatersSet, lat, lon, radius);
        }
    }
}
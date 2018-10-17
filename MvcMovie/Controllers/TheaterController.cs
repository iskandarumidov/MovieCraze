using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using MvcMovie.Models;
using NewtonJsonTest.Helpers;
//using NewtonJsonTest.Models;
using Newtonsoft.Json;

namespace MvcMovie.Controllers
{
    public class TheaterController : Controller
    {
        //
        // GET: /Theater/Find

        public ActionResult Find(string lat, string lon, string radius)
        {
            if (lat.IsFloat() && lon.IsFloat() && radius.IsFloat())
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                TheaterGetter getter = new TheaterGetter();


                HashSet<Theatre> uniqueTheatersSet = getter.getTheaters(today, lat, lon, radius, "km");
                //ViewBag.uniqueThs = uniqueTheatersSet;
                //string jsonOut = JsonConvert.SerializeObject(uniqueTheatersSet, Formatting.None);
                //ViewBag.jsonOut = jsonOut;
                //ViewBag.lat = uniqueTheatersSet.First().Latitude;
                return Content(JsonConvert.SerializeObject(uniqueTheatersSet, Formatting.None));
            }
            else
            {
                return Content("ERROR");
            }
            
        }

        public ActionResult Show()
        {
            return View();
        }

    }
}

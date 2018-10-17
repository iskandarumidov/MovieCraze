using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        [HttpPost]
        public ActionResult ContactBase(ContactModelBase model)
        {
            ViewBag.Message = "Your contact page.";
            if (ModelState.IsValid)
            {
                string response = "";
                
                response += "Thanks! You entered:<br>";
                //response += "<ul>";
                response += model.FirstName;
                response += "<br>";
                response += model.LastName;
                response += "<br>";
                response += model.Email;
                response += "<br>";
                response += model.Phone;
                response += "<br>";
                response += model.State;
                response += "<br>";
                response += model.FirstName;
                response += "<br>";
                response += model.Check18;
                response += "<br>";
                response += "NO AGE HERE";
                return Content(response);

            }
            

            return View("Contact");
        }


        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            ViewBag.Message = "Your contact page.";
            if (ModelState.IsValid)
            {
                string response = "";

                response += "Thanks! You entered:<br>";
                //response += "<ul>";
                response += model.FirstName;
                response += "<br>";
                response += model.LastName;
                response += "<br>";
                response += model.Email;
                response += "<br>";
                response += model.Phone;
                response += "<br>";
                response += model.State;
                response += "<br>";
                response += model.FirstName;
                response += "<br>";
                response += model.Check18;
                response += "<br>";
                response += model.Age;
                response += "<br>";
                return Content(response);

            }


            return View();
        }

        /*
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateAge(string Age, bool Check18)
        {
            int ageInt;
            if (!Check18)
            {
                if (Int32.TryParse(Age, out ageInt) && ageInt <= 100 && ageInt >= 0)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

            {
                return Json(true);
            }
            return Json(true);
        }*/
    }
}
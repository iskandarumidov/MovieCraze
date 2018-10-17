using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class SecretController : Controller
    {
        // GET: Secret
        [Authorize]
        public ActionResult Index()
        {
            return Content("This is a secret");
        }

        public ActionResult Overt()
        {
            return Content("Not secret");
        }
    }
}
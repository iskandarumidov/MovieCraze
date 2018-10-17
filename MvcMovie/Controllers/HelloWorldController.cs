using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        /*
        public ActionResult Create()
        {
            var countries = GetCountries();
            var model = new CountryViewModel {AvailableCountries = countries};
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CountryViewModel countryViewModel)
        {
            if (ModelState.IsValid)
            {
                int countryId = countryViewModel.SelectedCountryId;
                // Do something
            }

            // If we got this far, something failed. So, redisplay form
            countryViewModel.AvailableCountries = GetCountries();
            return View(countryViewModel);
        }

        public IList<SelectListItem> GetCountries()
        {
            // This comes from database.
            var _dbCountries = new List<Country>
            {
                new Country {Country_id = 1, Description = "USA"},
                new Country {Country_id = 2, Description = "UK"},
                new Country {Country_id = 3, Description = "Canada"},
            };
            var countries = _dbCountries
                .Select(x => new SelectListItem {Text = x.Description, Value = x.Country_id.ToString()})
                .ToList();
            countries.Insert(0, new SelectListItem {Text = "Choose a Country", Value = ""});
            return countries;
        }*/

        public ActionResult Index()
        {
            WebClient wc = new WebClient();
            string responseString = wc.DownloadString("");
            return View();
        }
    }
}

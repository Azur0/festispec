using Festispec.WebApplication.ViewModels;
using FestiSpec.SharedCode.Repositories;
using GoogleMaps.LocationServices;
using Newtonsoft.Json.Linq;
using ServiceStack;
using SharedCode.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Festispec.WebApplication2.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");

            Location location;
            LocationViewModel model;

            using (UnitOfWork uow = new UnitOfWork())
            { 
                location = uow.LocationRepository
                    .Get(u => u.Id == (int)Session["userID"])
                    .FirstOrDefault();

                model = new LocationViewModel() 
                { 
                    Number = location.Number, PostalCode = location.Postalcode 
                };

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(LocationViewModel model)
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Account");
            

            if (!ModelState.IsValid)
                return View(model);


            string googlekey = "AIzaSyB50QKDHggqCUGYGrdLy_NnlaUIudIH8w4";
            string address = $"{model.Number}, {model.PostalCode}, Nederland";

            GoogleLocationService locationService = new GoogleLocationService(apikey: googlekey);
            MapPoint point = locationService.GetLatLongFromAddress(address);

            double latitude = point.Latitude;
            double longitude = point.Longitude;

            string url = "https://maps.googleapis.com/maps/api/geocode/json?" +
                        $"latlng={latitude.ToString().Replace(',', '.')},{longitude.ToString().Replace(',', '.')}&" +
                        "sensor=false&" +
                        $"key={googlekey}";

            string json = url.GetJsonFromUrl();
            dynamic data = JObject.Parse(json);
            var city = data["results"][7]["address_components"][0].long_name.Value;
            
            using (UnitOfWork uow = new UnitOfWork())
            {
                User user = uow.UserRepository
                    .GetIncludes("Location", u => u.Id == 2)
                    .First();

                user.Location.Postalcode = model.PostalCode;
                user.Location.Number = model.Number;
                user.Location.City = city;
                user.Location.Longtitude = point.Longitude;
                user.Location.Latitude = point.Latitude;

                uow.UserRepository.Update(user);

                uow.Save();
            }

            model.ShowMessage = true;

            return View(model);
        }
    }
}
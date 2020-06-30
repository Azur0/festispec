using GoogleMaps.LocationServices;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public class GoogleLocation
    {
        private string API_KEY { get; set; }

        public GoogleLocation()
        {
            this.API_KEY = "AIzaSyB50QKDHggqCUGYGrdLy_NnlaUIudIH8w4";
        }

        public LocationLatLng GetLatLngFromAddressString(string address)
        {
            var locationService = new GoogleLocationService(apikey: this.API_KEY);
            var point = locationService.GetLatLongFromAddress(address);

            return new LocationLatLng() { Lat = point.Latitude, Lng = point.Longitude };
        }

        public double GetDistance(double originLatitude, double originLongitude, double destinationLatitude, double destinationLongitude)
        {

            var originGeocode = "https://maps.googleapis.com/maps/api/geocode/json?" +
                $"latlng={originLatitude.ToString().Replace(',', '.')},{originLongitude.ToString().Replace(',', '.')}&" +
                "sensor=false&" +
                $"key={API_KEY}";

            var destinationGeocode = "https://maps.googleapis.com/maps/api/geocode/json?" +
                $"latlng={destinationLatitude.ToString().Replace(',', '.')},{destinationLongitude.ToString().Replace(',', '.')}&" +
                "sensor=false&" +
                $"key={API_KEY}";

            string originJson = originGeocode.GetJsonFromUrl();
            string destinationJson = destinationGeocode.GetJsonFromUrl();

            dynamic originData = JObject.Parse(originJson);
            dynamic destinationData = JObject.Parse(destinationJson);

            var originAddress = originData["results"][0]["formatted_address"].Value;
            var destinationAddress = destinationData["results"][0]["formatted_address"].Value;

            var distanceMatrix = "https://maps.googleapis.com/maps/api/distancematrix/json?" +
                "units=metric&" +
                $"origins={originAddress}&" +
                $"destinations={destinationAddress}&" +
                $"key={API_KEY}";

            string distanceJson = distanceMatrix.GetJsonFromUrl();

            dynamic distanceData = JObject.Parse(distanceJson);

            var distance = Math.Round(Convert.ToDouble(distanceData["rows"][0]["elements"][0]["distance"]["value"].Value) / 1000, 2);

            return distance;
        }

    }

    public struct LocationLatLng
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

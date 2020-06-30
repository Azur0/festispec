using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Model Refrences
        public List<EventViewModel> events { get; set; }
        #endregion

        public List<Tuple<string, string, double, double>> GetLocations()
        {
            if (NetworkController.HasInternet() == false)
            {
                return new List<Tuple<string, string, double, double>>();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                List<Tuple<string, string, double, double>> locationData = new List<Tuple<string, string, double, double>>();

                // Inspectors
                uow.UserRepository
                    .GetIncludes("Location", user => user.UserRole == "inspector")
                    .Select(user => new { name = $"{user.FirstName} {user.LastName}", type = "inspector", user.Location.Latitude, user.Location.Longtitude })
                    .ToList()
                    .ForEach(u => locationData.Add(new Tuple<string, string, double, double>(u.name, u.type, u.Latitude.Value, u.Longtitude.Value)));

                // Customers
                uow.CustomerRepository
                    .GetIncludes("Location")
                    .Select(customer => new { name = $"{customer.Name}", type = "customer", customer.Location.Latitude, customer.Location.Longtitude })
                    .ToList()
                    .ForEach(c => locationData.Add(new Tuple<string, string, double, double>(c.name, c.type, c.Latitude.Value, c.Longtitude.Value)));

                // Inspections
                uow.EventRepository
                    .GetIncludes("Location")
                    .Select(_event => new { name = $"{_event.Name}", type = "event", _event.Location.Latitude, _event.Location.Longtitude })
                    .ToList()
                    .ForEach(e => locationData.Add(new Tuple<string, string, double, double>(e.name, e.type, e.Latitude.Value, e.Longtitude.Value)));


                return locationData.ToList();
            }
        }

        public List<dynamic> GetAvailability()
        {
            using (UnitOfWork uow = new UnitOfWork())
            { 
                return uow.UserHasAvailibiltyRepository
                    .GetIncludes("User")
                    .Select(availibility =>
                    {

                        dynamic cool = new
                        {
                            time = availibility.Day,
                            user = availibility.User.FirstName + " " + availibility.User.LastName
                        };
                        return cool;

                    }).ToList();
            }
        }
	}
}

using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES

        private Customer _customer;

        #endregion

        #region CONSTRUCTOR
        public CustomerViewModel(Customer customer = null)
        {
            if (customer == null)
            {
                customer = new Customer
                {
                    Location = new Location()
                };
            }

            this._customer = customer;
        }
        #endregion

        #region PUBLIC PROPERTIES

        public int Id 
        {
            get => this._customer.Id;
        }

        public int LocationId 
        {
            get => this._customer.LocationId;
            set { this._customer.LocationId = value; }
        }
        public string Name 
        {
            get => this._customer.Name;
            set {
				value = value.Trim();
				this._customer.Name = (string)Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
				RaisePropertyChanged("Name");
			}
        }
		public string Email
		{
			get
			{
				return this._customer.Email;
			}
			set
			{
				value = value.Trim();
				this._customer.Email = (string)Validators.Check(value, "Email", "NotEmpty|MaxLength:255|Email");
				RaisePropertyChanged("Email");
			}
		}
		public string TelephoneNumber 
        {
            get => this._customer.TelephoneNumber;
            set {
				value = value.Trim();
				this._customer.TelephoneNumber = (string)Validators.Check(value, "Telefoon", "NotEmpty|ExactLength:10");
				RaisePropertyChanged("TelephoneNumber");
			}
        }
        public string Kvk 
        {
            get => this._customer.Kvk;
            set 
			{
				value = value.Trim();
				this._customer.Kvk = (string)Validators.Check(value, "Kvk", "NotEmpty|ExactLength:8");
				RaisePropertyChanged("Kvk");
			}
        }
		public string PostalCode
		{
			get => this._customer.Location.Postalcode;
			set
			{
				value = value.Trim();
				this._customer.Location.Postalcode = (string)Validators.Check(value, "Postcode", "NotEmpty|PostalCode");
				RaisePropertyChanged("PostalCode");
			}
		}

		public string Number
		{
			get => this._customer.Location.Number;
			set
			{
				value = value.Trim();
				this._customer.Location.Number = (string)Validators.Check(value, "Huisnummer", "NotEmpty|MaxLength:45");
				RaisePropertyChanged("Password");
			}
		}

		public string City
		{
			get => this._customer.Location.City;
			set
			{
				value = value.Trim();
				this._customer.Location.City = (string)Validators.Check(value, "Plaats", "NotEmpty|MaxLength:45");
				RaisePropertyChanged("City");
			}
		}

        public string FullAddress
        {
            get => $"{PostalCode}, {City}";
        }
        public DateTime CreatedAt 
        {
            get => this._customer.CreatedAt;
            set { this._customer.CreatedAt = value; }
        }
        public DateTime? UpdatedAt 
        {
            get => this._customer.UpdatedAt;
            set { this._customer.UpdatedAt = value; }
        }
        public DateTime? DeletedAt 
        {
            get => this._customer.DeletedAt;
            set { this._customer.DeletedAt = value; }
        }

		public ICollection<Event> Event
		{
			get => this._customer.Event;
		}
		#endregion

		#region METHODS

		public void LoadCustomerByPK(int customerId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this._customer = unitOfWork.CustomerRepository.Get(u => u.Id == customerId).First();
                this._customer.Location = unitOfWork.LocationRepository.Get(l => l.Id == this._customer.LocationId).First();
                this._customer.Event = unitOfWork.EventRepository.Get(a => a.CustomerId == this._customer.Id).ToList();
            }
        }

        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.CustomerRepository.Insert(this._customer);
                unitOfWork.Save();
            }
        }

        public void Update()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.CustomerRepository.Update(this._customer);
                unitOfWork.Save();
            }
        }

        private void LinkLocation(UnitOfWork unitOfWork)
        {
            Location dbLocation;
            // get same location from database
            dbLocation = unitOfWork.LocationRepository
                .Get(l => l.Postalcode == this._customer.Location.Postalcode && l.Number == this._customer.Location.Number && l.City == this._customer.Location.City)
                .FirstOrDefault();

            // if location not exists in database then insert the location in the database
            if (dbLocation == null)
            {
                // get lng lot
                LocationLatLng points = new GoogleLocation().GetLatLngFromAddressString($"{this._customer.Location.Postalcode}, {this._customer.Location.City}, Nederland ");

                this._customer.Location = new Location()
                {
                    Postalcode = this._customer.Location.Postalcode,
                    Number = this._customer.Location.Number,
                    City = this._customer.Location.City,
                    Latitude = points.Lat,
                    Longtitude = points.Lng,
                    CreatedAt = this._customer.Location.CreatedAt,
                    UpdatedAt = this._customer.Location.UpdatedAt
                };

                unitOfWork.LocationRepository.Insert(this._customer.Location);
                unitOfWork.Save();

                // get location
                dbLocation = unitOfWork.LocationRepository
                    .Get(l => l.Postalcode == this._customer.Location.Postalcode && l.Number == this._customer.Location.Number && l.City == this._customer.Location.City)
                    .FirstOrDefault();                
            }
            // set user location to database location id
            this._customer.LocationId = dbLocation.Id;
        }

		public List<Tuple<string, string, double, double>> GetLocations()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var locationData = new List<Tuple<string, string, double, double>>();

                // Customers
                uow.CustomerRepository
                    .GetIncludes("Location")
                    .Where(x => x.Id == this._customer.Id)
                    .Select(customer => new { name = $"{customer.Name}", type = "customer", customer.Location.Latitude, customer.Location.Longtitude })
                    .ToList()
                    .ForEach(c => locationData.Add(new Tuple<string, string, double, double>(c.name, c.type, c.Latitude.Value, c.Longtitude.Value)));

                // Inspections
                uow.EventRepository
                    .GetIncludes("Location")
                    .Where(x => x.CustomerId == this._customer.Id)
                    .Where(x => x.EndDate > DateTime.Now)
                    .Select(_event => new { name = $"{_event.Name}", type = "event", _event.Location.Latitude, _event.Location.Longtitude })
                    .ToList()
                    .ForEach(e => locationData.Add(new Tuple<string, string, double, double>(e.name, e.type, e.Latitude.Value, e.Longtitude.Value)));

                return locationData.ToList();
            }
        }

        #endregion
    }
}
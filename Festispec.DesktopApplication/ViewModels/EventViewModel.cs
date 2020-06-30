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
    public class EventViewModel : ViewModelBase
    {
        #region PRIVATE PROPS
        private Event _event;
        #endregion

        #region CONST
        public EventViewModel(Event eventt = null)
        {
            if (eventt == null)
            {
                this._event = new Event();
                this._event.Location = new Location();
                this.PaymentStatus = "niet betaald";
                this.EventStatus = "geinitialiseert";
                this.StartDate = DateTime.Now;
                this.EndDate = DateTime.Now;
            }
            else
            {
                this._event = eventt;
            }
        }
        #endregion

        #region PUBLIC PROPS

        public List<CustomerViewModel> Customers { get; private set; }

        public List<QuotationViewModel> Quotations { get; private set; }

        public List<string> PaymentStatusList { get; private set; }

        public int Id 
        {
            get => this._event.Id;
        }

        #region CUSTOMER OPTION AND ID
        private Tuple<object, string> _customerOption { get; set; }
        public Tuple<object, string> CustomerOption
        {
            get => this._customerOption;
            set
            {
                object option = (this._customerOption == null ? null : this._customerOption.Item1);
                if ((option != null) && (option.ToString() == value.Item1.ToString()))
                {
                    return;
                }
                this._customerOption = value;
                this.CustomerId = (int)_customerOption.Item1;
                RaisePropertyChanged("CustomerOption");
            }
        }

        public int CustomerId
        {
            get => this._event.CustomerId;
            set
            {
                this._event.CustomerId = value;
                this.CustomerOption = new Tuple<object, string>(value, value.ToString());
                RaisePropertyChanged("CustomerId");
            }
        }
        #endregion

        #region PAYMENT OPTION AND STRING
        private Tuple<object, string> _paymentOption { get; set; }
        public Tuple<object, string> PaymentOption
        {
            get => this._paymentOption;
            set
            {
                object option = (this._paymentOption == null ? null : this._paymentOption.Item1);
                if ((option != null) && (option.ToString() == value.Item1.ToString()))
                    return;
                this._paymentOption = value;
                this.PaymentStatus = _paymentOption.Item1.ToString();
                RaisePropertyChanged("PaymentOption");
            }
        }

        public string PaymentStatus
        {
            get => this._event.PaymentStatus;
            set
            {
                this._event.PaymentStatus = value;
                this.PaymentOption = new Tuple<object, string>(value, value.ToString());
                RaisePropertyChanged("PaymentStatus");
            }
        }
        #endregion

 
        public string CustomerName
        {
            get => (this._event.Customer == null ? "LEEG" : this._event.Customer.Name);
        }

        public string PostalCode
        {
            get => this._event.Location.Postalcode;
            set
            {
                value = value.Trim();
                this._event.Location.Postalcode = (string)Validators.Check(value, "Postcode", "NotEmpty|PostalCode");
                RaisePropertyChanged("PostalCode");
            }
        }

        public string Number
        {
            get => this._event.Location.Number;
            set
            {
                value = value.Trim();
                this._event.Location.Number = (string)Validators.Check(value, "Huisnummer", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("Number");
            }
        }

        public string City
        {
            get => this._event.Location.City;
            set
            {
                value = value.Trim();
                this._event.Location.City = (string)Validators.Check(value, "Plaats", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("City");
            }
        }

        public string EventStatus 
        {
            get => this._event.EventStatus;
            set 
            { 
                this._event.EventStatus = value; 
            }
        }

        public string Name 
        {
            get => this._event.Name;
            set {
                value = value.Trim();
                this._event.Name = (string)Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("Name");
            }
        }
        public DateTime StartDate 
        {
            get => this._event.StartDate;
            set {
                this._event.StartDate = value;
            }
        }
        public DateTime EndDate 
        {
            get => this._event.EndDate;
            set {
                this._event.EndDate = value;
            }
            
        }
        public string About 
        {
            get => this._event.About;
            set
            {
                value = value.Trim();
                this._event.About = (string)Validators.Check(value, "Omschrijving", "MaxLength:250");
                RaisePropertyChanged("About");
            } //will not validate < billions of chars...
        }

        

        #endregion

        #region ACTIONS

        public void LoadEventByPK(int pk)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._event = uow.EventRepository.GetIncludes("Location", x => x.Id == pk).First();
                this._event.Customer = uow.CustomerRepository.Get().Where(c => c.Id == this._event.CustomerId).First();
            }
        }

        public void LoadQuoations()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this.Quotations = uow.QuotationRepository
                    .Get().Where(q => q.EventId == _event.Id)
                    .Select(q => new QuotationViewModel(q))
                    .ToList();
            }
        }
        public void LoadCustomers()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this.Customers = uow.CustomerRepository
                    .Get()
                    .Select(c => new CustomerViewModel(c))
                    .ToList();
            }
        }

        public void LoadPaymentStatus()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this.PaymentStatusList = uow.PaymentStatusRepository
                    .Get()
                    .Select(status => status.PaymentStatus1)
                    .ToList();
            }
        }

        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                this.LinkLocation(unitOfWork);
                unitOfWork.EventRepository.Insert(this._event);
                unitOfWork.Save();
            }
        }

        public void Update()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            this.LinkLocation(unitOfWork);
            unitOfWork.EventRepository.Update(this._event);
            unitOfWork.Save();
        }

        private void LinkLocation(UnitOfWork unitOfWork)
        {
            Location dbLocation;
            using (unitOfWork)
            { 
                // get same location from database
                dbLocation = unitOfWork.LocationRepository
                    .Get(l => l.Postalcode == this._event.Location.Postalcode && l.Number == this._event.Location.Number && l.City == this._event.Location.City)
                    .FirstOrDefault();

                // if location not exists in database then insert the location in the database
                if (dbLocation == null)
                {
                    // get lng lot
                    LocationLatLng points = new GoogleLocation().GetLatLngFromAddressString($"{this._event.Location.Postalcode}, {this._event.Location.City}, Nederland ");

                    this._event.Location = new Location()
                    {
                        Postalcode = this._event.Location.Postalcode,
                        Number = this._event.Location.Number,
                        City = this._event.Location.City,
                        Latitude = points.Lat,
                        Longtitude = points.Lng,
                        CreatedAt = this._event.Location.CreatedAt,
                        UpdatedAt = this._event.Location.UpdatedAt
                    };

                    unitOfWork.LocationRepository.Insert(this._event.Location);
                    unitOfWork.Save();
                }

                // get location
                dbLocation = unitOfWork.LocationRepository
                    .Get(l => l.Postalcode == this._event.Location.Postalcode && l.Number == this._event.Location.Number && l.City == this._event.Location.City)
                    .FirstOrDefault();
            }

            // set user location to database location id
            this._event.LocationId = dbLocation.Id;
        }

        public Event ToModel()
        {
            return this._event;
        }

        #endregion
    }

}

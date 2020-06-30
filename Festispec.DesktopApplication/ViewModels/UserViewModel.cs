using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Festispec.DesktopApplication.DesktopControllers;

using FestiSpec.SharedCode.Repositories;


namespace Festispec.DesktopApplication.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES
        private User _user;
        private List<UserRoleViewModel> _roles;
        #endregion

        #region CONSTRUCTORS

        public UserViewModel(User user = null)
        {
            if (user == null)
            {
                user = new User();
                user.Location = new Location();
            }

            this._user = user;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public int Id
        {
            get => this._user.Id;
        }

        public List<UserRoleViewModel> Roles { get => _roles; }

        private Tuple<object, string> _userRoleOption { get; set; }
        public Tuple<object, string> UserRoleOption
        {
            get => this._userRoleOption;
            set
            {
                if (value == null) return;
                if ((this._userRoleOption == null ? null : this._userRoleOption.Item1) == value.Item1) return;
                this._userRoleOption = value;
                this.UserRole = (string)_userRoleOption.Item1;
                RaisePropertyChanged("UserRoleOption");
            }
        }

        public string UserRole
        {
            get => this._user.UserRole;
            set
            {
                value = value.Trim();
                value = (string)Validators.Check(value, "Rol", "NotEmpty|MaxLength:45");
                this._user.UserRole = value;

                this.UserRoleOption = new Tuple<object, string>(value, value);

                RaisePropertyChanged("UserRole");
            }
        }

        public string FirstName
        {
            get => this._user.FirstName;
            set
            {
                value = value.Trim();
                this._user.FirstName = (string) Validators.Check(value, "Voornaam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("FirstName");

            }
        }

        public string MiddleName
        {
            get => this._user.MiddleName;
            set
            {
                if (value == null) return;
                value = value.Trim();
                this._user.MiddleName = (string) Validators.Check(value, "Tussenvoegsel", "MaxLength:45");
                RaisePropertyChanged("MiddleName");
            }
        }

        public string LastName
        {
            get => this._user.LastName;
            set
            {
                value = value.Trim();
                this._user.LastName = (string) Validators.Check(value, "Achternaam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("LastName");
            }
        }

        public string FullName
        {
            get {
                if (MiddleName == null) return $"{FirstName} {LastName}";
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        public string Email
        {
            get {
                return this._user.Email;
            }
            set
            {
                value = value.Trim();
                this._user.Email = (string) Validators.Check(value, "Email", "NotEmpty|MaxLength:255|Email");
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get => this._user.Password;
            set
            {
                if (value == null) return;
                value = value.Trim();
                this._user.Password = (string) Validators.Check(value, "Wachtwoord", "NotEmpty|MixLength:8|MaxLength:45");
                RaisePropertyChanged("Password");
            }
        }


		public string PostalCode
		{
			get => this._user.Location?.Postalcode;
            set
			{
                if (value == null) return;
                value = value.Trim();
                this._user.Location.Postalcode = (string) Validators.Check(value, "Postcode", "NotEmpty|PostalCode");
                RaisePropertyChanged("PostalCode");
			}
		}

		public string Number
		{
			get => this._user.Location?.Number;
			set
			{
                if (value == null) return;
                value = value.Trim();
                this._user.Location.Number = (string) Validators.Check(value, "Huisnummer", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("Password");
			}
		}

		public string City
		{
			get => this._user.Location?.City;
            set
			{
                if (value == null) return;
                value = value.Trim();
                this._user.Location.City = (string) Validators.Check(value, "Plaats", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("City");
			}
		}

        public string FullAddress
        {
            get => $"{PostalCode}, {City}";
        }

        #endregion

        #region METHODS
        public void LoadUserByPK(int pk)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._user = uow.UserRepository
                    .GetIncludes("Location", x => x.Id == pk)
                    .First();
            }
        }

        public void LoadRoles()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._roles = uow.UserRoleRepository
                    .Get()
                    .Select(r => new UserRoleViewModel(r))
                    .ToList();
            }
        }

        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // encrypt password
                this._user.Password = Crypt.SHA256(this._user.Password);

                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.UserRepository
                    .Insert(this._user);
            }
        }

        public void Update()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.UserRepository
                    .Update(this._user);
            }
        }

        private void LinkLocation(UnitOfWork unitOfWork)
        {
            // get same location from database
            var dbLocation = unitOfWork.LocationRepository
                .Get(l => l.Postalcode == this._user.Location.Postalcode && l.Number == this._user.Location.Number && l.City == this._user.Location.City)
                .FirstOrDefault();

            // if location not exists in database then insert the location in the database
            if (dbLocation == null)
            {
                // get lng lot
                var points = new GoogleLocation()
                    .GetLatLngFromAddressString($"{this._user.Location.Postalcode}, {this._user.Location.City}, Nederland ");

                this._user.Location = new Location()
                {
                    Postalcode = this._user.Location.Postalcode,
                    Number = this._user.Location.Number,
                    City = this._user.Location.City,
                    Latitude = points.Lat,
                    Longtitude = points.Lng,
                    CreatedAt = this._user.Location.CreatedAt,
                    UpdatedAt = this._user.Location.UpdatedAt
                };

                unitOfWork.LocationRepository.Insert(this._user.Location);
                
                unitOfWork.Save();

                // get location
                dbLocation = unitOfWork.LocationRepository
                    .Get(l => l.Postalcode == this._user.Location.Postalcode && l.Number == this._user.Location.Number && l.City == this._user.Location.City)
                    .FirstOrDefault();
            }

            // set user location to database location id
            this._user.LocationId = dbLocation.Id;
        }

        #endregion
    }
}
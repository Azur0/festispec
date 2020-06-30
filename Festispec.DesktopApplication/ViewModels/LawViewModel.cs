using Festispec.DesktopApplication.DesktopControllers;
using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class LawViewModel : ViewModelBase
    {
        #region PRIVATE PROPERTIES

        private Law _law;

        #endregion

        #region CONSTRUCTOR
        public LawViewModel(Law law = null)
        {
            if (law == null)
            {
                law = new Law();
                law.Location = new Location();
            }

            this._law = law;
        }
        #endregion

        #region PUBLIC PROPERTIES

        public int Id
        {
            get => this._law.Id;
        }

        public int? LocationId
        {
            get => this._law.LocationId;
            set { this._law.LocationId = value; }
        }
        public string Name
        {
            get => this._law.Name;
            set
            {
                value = value.Trim();
                this._law.Name = (string)Validators.Check(value, "Naam", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("Law1");
            }
        }

        public string Content
        {
            get => this._law.Content;
            set
            {
                value = value.Trim();
                this._law.Content = (string)Validators.Check(value, "Content", "NotEmpty|MaxLength:250");
                RaisePropertyChanged("Content");
            }
        }

        public string City
        {
            get => this._law.Location.City;
            set
            {
                value = value.Trim();
                this._law.Location.City = (string)Validators.Check(value, "Plaats", "NotEmpty|MaxLength:45");
                RaisePropertyChanged("City");
            }
        }


        #endregion

        #region METHODS

        public void LoadLawByPK(int pk)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                this._law = uow.LawRepository.GetIncludes("Location", x => x.Id == pk).First();
            }
        }


        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.LawRepository.Insert(this._law);
                unitOfWork.Save();
            }
        }

        public void Update()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // link the right location
                this.LinkLocation(unitOfWork);

                unitOfWork.LawRepository.Update(this._law);
                unitOfWork.Save();
            }
        }

        private void LinkLocation(UnitOfWork unitOfWork)
        {
            // get same location from database
            Location dbLocation = unitOfWork.LocationRepository
                .Get(r => r.City == this._law.Location.City)
                .FirstOrDefault();

            // if location not exists in database then insert the location in the database
            if (dbLocation == null)
            {
                // get lng lot
                LocationLatLng points = new GoogleLocation().GetLatLngFromAddressString($"{this._law.Location}, {this._law.Location.City}, Nederland ");

                this._law.Location = new Location()
                {
                    City = this._law.Location.City,
                    Latitude = points.Lat,
                    Longtitude = points.Lng,
                    CreatedAt = this._law.Location.CreatedAt,
                    UpdatedAt = this._law.Location.UpdatedAt
                };

                unitOfWork.LocationRepository.Insert(this._law.Location);
                unitOfWork.Save();

                // get location
                dbLocation = unitOfWork.LocationRepository
                    .Get(l =>  l.City == this._law.Location.City)
                    .FirstOrDefault();
            }

            // set user location to database location id
            this._law.LocationId = dbLocation.Id;
        }

        #endregion

    }
}

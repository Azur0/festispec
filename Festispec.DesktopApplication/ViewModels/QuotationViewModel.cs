using FestiSpec.SharedCode.Repositories;
using GalaSoft.MvvmLight;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.DesktopApplication.ViewModels
{
    public class QuotationViewModel : ViewModelBase
    {
        #region Model References

        private Quotation _quotation;

        private List<string> _status;

        public List<string> QuotationStatusList { get => _status; }



        #endregion

        #region Getters & Setters

        public int Id
        {
            get => this._quotation.Id;
        }
        public int EventId
        {
            get => this._quotation.EventId;
            set
            {
                _quotation.EventId = value;
            }
        }
        public double Price 
        {
            get => this._quotation.Price; 
            set
            {
                this._quotation.Price = value;
            }
        }
       
        public string Text 
        {
            get => this._quotation.Text;
            set
            {
                this._quotation.Text = value;
            }
        }

        public string EventName
        {
            get => _quotation.Event.Name;
        }

        public virtual EventViewModel Event 
        {
            get => new EventViewModel(this._quotation.Event);
            set
            {
                this._quotation.Event = value.ToModel();
            }
        }

        public virtual QuotationStatusViewModel QuotationStatusStatusNavigation 
        {
            get => new QuotationStatusViewModel(this._quotation.QuotationStatusStatusNavigation);
            set
            {
                this._quotation.QuotationStatusStatusNavigation = value.ToModel();
            }
        }

        private Tuple<object, string> _QuotationOption { get; set; }
        public Tuple<object, string> QuotationOption
        {
            get => this._QuotationOption;
            set
            {
                var option = (this._QuotationOption == null ? null : this._QuotationOption.Item1);
                if ((option != null) && (option.ToString() == value.Item1.ToString()))
                {
                    return;
                }
                this._QuotationOption = value;
                this.QuotationStatus = _QuotationOption.Item1.ToString();
                RaisePropertyChanged("QuotationOption");
            }
        }

        public string QuotationStatus
        {
            get => this._quotation.QuotationStatusStatus;
            set
            {
                this._quotation.QuotationStatusStatus = value;
                this.QuotationOption = new Tuple<object, string>(value, value.ToString());
                RaisePropertyChanged("QuotationStatus");
            }
        }

        public void LoadQuotationByPK(int pk)
        {
            UnitOfWork uow = new UnitOfWork();

            _quotation = uow.QuotationRepository.Get(q => q.Id == pk).First();
            Event = new EventViewModel(uow.EventRepository.Get(q => q.Id == _quotation.EventId).First());
            uow.Dispose();
        }

        public void LoadStatus()
        {
            UnitOfWork uow = new UnitOfWork();

            this._status = uow.QuotationStatusRepository
                .Get()
                .Select(c => c.Status)
                .ToList();

            uow.Dispose();
        }
        #endregion

        public QuotationViewModel(Quotation quotation = null)
        {
            if(quotation == null)
            {
                this._quotation = new Quotation();
                this._quotation.Event = new Event();
            }
            else
            {
                this._quotation = quotation;
            }
            
        }


        public void Update()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.QuotationRepository.Update(this._quotation);
                
                uow.Save();
            }
        }

        public void Insert()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                // gare workaround
                int eventId = this._quotation.Event.Id;
                this._quotation.Event = null;
                this._quotation.EventId = eventId;
                this._quotation.QuotationStatusStatus = "in afwachting";

                unitOfWork.QuotationRepository.Insert(this._quotation);
                unitOfWork.Save();
            }
        }
    }
}

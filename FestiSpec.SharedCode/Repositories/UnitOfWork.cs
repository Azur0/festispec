using SharedCode.Models;
using SharedCode.Repositories;
using System;
using System.Net.NetworkInformation;

namespace FestiSpec.SharedCode.Repositories
{
    public class UnitOfWork : IDisposable
    {
        #region Repositories

        private FestispecContext context = new FestispecContext();

        private GenericRepository<Answer> _answerRepository;
        private GenericRepository<Assignees> _assigneesRepository;
        private SoftDeleteGenericRepository<Customer> _customerRepository;
        private SoftDeleteGenericRepository<Event> _eventRepository;
        private SoftDeleteGenericRepository<EventInspection> _eventInspectionRepository;
        private GenericRepository<EventStatus> _eventStatusRepository;
        private SoftDeleteGenericRepository<FileLink> _fileLinkRepository;
        private SoftDeleteGenericRepository<FormQuestion> _formQuestionRepository;
        private SoftDeleteGenericRepository<InspectionForm> _inspectionFormRepository;
        private GenericRepository<Law> _lawRepository;
        private SoftDeleteGenericRepository<Location> _locationRepository;
        private GenericRepository<Multplechoice> _multipleChoiceRepository;
        private GenericRepository<PaymentStatus> _paymentStatusRepository;
        private SoftDeleteGenericRepository<Quotation> _quotationRepository;
        private GenericRepository<QuotationStatus> _quotationStatusRepository;
        private GenericRepository<QuestionType> _questionTypeRepository;
        private SoftDeleteGenericRepository<User> _userRepository;
        private GenericRepository<UserHasAvailability> _userHasAvailabilityRepository;
        private GenericRepository<UserRole> _userRoleRepository;
        private GenericRepository<ReportHasEvents> _reportHasEventsRepository;
        private SoftDeleteGenericRepository<Report> _reportRepository;
        


        public GenericRepository<Answer> AnswerRepository
        {
            get
            {
                if (_answerRepository == null) _answerRepository = new GenericRepository<Answer>(context);
                return _answerRepository;
            }
        }
        public GenericRepository<Assignees> AssigneesRepository
        {
            get
            {
                if (_assigneesRepository == null) _assigneesRepository = new GenericRepository<Assignees>(context);
                return _assigneesRepository;
            }
        }
        public SoftDeleteGenericRepository<Customer> CustomerRepository
        {
            get 
            {
                if (_customerRepository == null) _customerRepository = new SoftDeleteGenericRepository<Customer>(context);
                return _customerRepository;
            }
        }
        public SoftDeleteGenericRepository<Event> EventRepository
        {
            get
            {
                if (_eventRepository == null) _eventRepository = new SoftDeleteGenericRepository<Event>(context);
                return _eventRepository;
            }
        }
        public SoftDeleteGenericRepository<EventInspection> EventInspectionRepository
        {
            get
            {
                if (_eventInspectionRepository == null) _eventInspectionRepository = new SoftDeleteGenericRepository<EventInspection>(context);
                return _eventInspectionRepository;
            }
        }
        public GenericRepository<EventStatus> EventStatusRepository
        {
            get
            {
                if (_eventStatusRepository == null) _eventStatusRepository = new GenericRepository<EventStatus>(context);
                return _eventStatusRepository;
            }
        }
        public SoftDeleteGenericRepository<FileLink> FileLinkRepository
        {
            get
            {
                if (_fileLinkRepository == null) _fileLinkRepository = new SoftDeleteGenericRepository<FileLink>(context);
                return _fileLinkRepository;
            }
        }
        public SoftDeleteGenericRepository<FormQuestion> FormQuestionRepository
        {
            get
            {
                if (_formQuestionRepository == null) _formQuestionRepository = new SoftDeleteGenericRepository<FormQuestion>(context);
                return _formQuestionRepository;
            }
        }
        public SoftDeleteGenericRepository<InspectionForm> InspectionFormRepository
        {
            get
            {
                if (_inspectionFormRepository == null) _inspectionFormRepository = new SoftDeleteGenericRepository<InspectionForm>(context);
                return _inspectionFormRepository;
            }
        }
        public GenericRepository<Law> LawRepository
        {
            get
            {
                if (_lawRepository == null) _lawRepository = new GenericRepository<Law>(context);
                return _lawRepository;
            }
        }
        public SoftDeleteGenericRepository<Location> LocationRepository
        {
            get
            {
                if (_locationRepository == null) _locationRepository = new SoftDeleteGenericRepository<Location>(context);
                return _locationRepository;
            }
        }
        public GenericRepository<Multplechoice> MultipleChoiceRepository
        {
            get
            {
                if (_multipleChoiceRepository == null) _multipleChoiceRepository = new GenericRepository<Multplechoice>(context);
                return _multipleChoiceRepository;
            }
        }
        public GenericRepository<PaymentStatus> PaymentStatusRepository
        {
            get
            {
                if (_paymentStatusRepository == null) _paymentStatusRepository = new GenericRepository<PaymentStatus>(context);
                return _paymentStatusRepository;
            }
        }
        public SoftDeleteGenericRepository<Quotation> QuotationRepository
        {
            get
            {
                if (_quotationRepository == null) _quotationRepository = new SoftDeleteGenericRepository<Quotation>(context);
                return _quotationRepository;
            }
        }
        public GenericRepository<QuotationStatus> QuotationStatusRepository
        {
            get
            {
                if (_quotationStatusRepository == null) _quotationStatusRepository = new GenericRepository<QuotationStatus>(context);
                return _quotationStatusRepository;
            }
        }
        public GenericRepository<QuestionType> QuestionTypeRepository
        {
            get
            {
                if (_questionTypeRepository == null) _questionTypeRepository = new GenericRepository<QuestionType>(context);
                return _questionTypeRepository;
            }
        }
        public SoftDeleteGenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null) _userRepository = new SoftDeleteGenericRepository<User>(context);
                return _userRepository;
            }
        }
        public GenericRepository<UserHasAvailability> UserHasAvailibiltyRepository
        {
            get
            {
                if (_userHasAvailabilityRepository == null) _userHasAvailabilityRepository = new GenericRepository<UserHasAvailability>(context);
                return _userHasAvailabilityRepository;
            }
        }
        public GenericRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null) _userRoleRepository = new GenericRepository<UserRole>(context);
                return _userRoleRepository;
            }
        }
        public GenericRepository<ReportHasEvents> ReportHasEventsRepository
        {
            get
            {
                if (_reportHasEventsRepository == null) _reportHasEventsRepository = new GenericRepository<ReportHasEvents>(context);
                return _reportHasEventsRepository;
            }
        }
        public SoftDeleteGenericRepository<Report> ReportRepository
        {
            get
            {
                if (_reportRepository == null) _reportRepository = new SoftDeleteGenericRepository<Report>(context);
                return _reportRepository;
            }
        }
        #endregion

        private bool disposed = false;

        public void Save() => context.SaveChanges();
                
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
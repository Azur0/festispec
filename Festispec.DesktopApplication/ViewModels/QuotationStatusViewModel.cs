using SharedCode.Models;

namespace Festispec.DesktopApplication.ViewModels
{
    public class QuotationStatusViewModel
    {
        private QuotationStatus _quotationStatusStatus;

        public QuotationStatusViewModel(QuotationStatus quotationStatusStatusNavigation)
        {
            this._quotationStatusStatus = quotationStatusStatusNavigation;
        }

        public QuotationStatus ToModel() => this._quotationStatusStatus;        
    }
}
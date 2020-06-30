using GalaSoft.MvvmLight;
using SharedCode.Models;

namespace Festispec.DesktopApplication.ViewModels
{
    public class PaymentStatusViewModel  : ViewModelBase
    {
        private PaymentStatus _paymentStatus;

        public PaymentStatusViewModel(PaymentStatus paymentStatus)
        {
            this._paymentStatus = paymentStatus;
        }

        public string PaymentStatus
        {
            get => this._paymentStatus.PaymentStatus1;
            set { this._paymentStatus.PaymentStatus1 = value; }
        }
    }
}

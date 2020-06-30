using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.DesktopApplication.ViewModels
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
        }
        public CustomerViewModel CustomerVM
        {
            get => ServiceLocator.Current.GetInstance<CustomerViewModel>();
        }
    }
}

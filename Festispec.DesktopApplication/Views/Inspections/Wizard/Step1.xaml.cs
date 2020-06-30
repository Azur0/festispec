using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Festispec.DesktopApplication.ViewModels;
using Festispec.DesktopApplication.DesktopControllers;
using System;
using Festispec.DesktopApplication.Views.Components.Dropdown;
using FestiSpec.SharedCode.Repositories;
using SharedCode.Models;
using System.Linq;
using Windows.UI.Xaml.Navigation;

namespace Festispec.DesktopApplication.Views.Inspections.Wizard
{
    public sealed partial class Step1 : Page
    {
        public InspectionViewModel inspectionViewModel { get; set; }
        DropDownItems dropDownItems { get; set; }

        public Step1()
        {
            this.InitializeComponent();
            this.dropDownItems = new DropDownItems();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.inspectionViewModel = (e.Parameter as InspectionViewModel);
            

            if (this.inspectionViewModel.Events.Count == 0)
            {
                this.inspectionViewModel.LoadEvents();
            }

            PageHelper.MainUI(Dispatcher, () =>
            {
                this.inspectionViewModel.Events.ForEach(Event =>
                {
                    dropDownItems.AddItem(Event.Id, Event.Name);
                });

                if (this.inspectionViewModel.EventId != 0)
                {
                    var a = dropDownItems.GetOptions.Where(option => option.Item1.ToString() == this.inspectionViewModel.EventId.ToString()).First();
                    this.inspectionViewModel.EventId = 0;
                    this.inspectionViewModel.EventOption = a;
                    this.Bindings.Update();
                }

            });

        }
    }
}

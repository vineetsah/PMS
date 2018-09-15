using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> NavigateCommand { get; set; }
        public MainWindowViewModel(IRegionManager _regionManager)
        {
            this.regionManager = _regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate,canExecuteMethod);
        }

        private bool canExecuteMethod(string arg)
        {
            return true;
        }

        private void Navigate(string uri)
        {
            regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}

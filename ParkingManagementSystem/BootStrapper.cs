using Microsoft.Practices.Unity;
using ParkingManagementSystem.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingManagementSystem
{
    public class BootStrapper:UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //Container.RegisterType(typeof(object), typeof(Views.ViewEntry), "ViewEntry");
            //Container.RegisterType(typeof(object), typeof(Views.ViewEntry), "ViewExit");
            Container.RegisterTypeForNavigation<ViewEntry>("ViewEntry");
            Container.RegisterTypeForNavigation<ViewExit>("ViewExit");
        }
    }
}

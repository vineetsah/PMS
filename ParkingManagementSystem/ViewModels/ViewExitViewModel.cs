using ParkingManagementSystem.Constant;
using PMSBusinessLayer;
using PMSModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;

namespace ParkingManagementSystem.ViewModels
{
    public class ViewExitViewModel : BindableBase, IDataErrorInfo
    {
        const double ChargesPerHrs_FourWheeler = 50;
        const double ChargesPerHrs_TwoWheeler = 20;
        private string title = "PMS Exit Details";
        Vehicle_Parking parkedVehicle_;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Visibility borderVisibility=Visibility.Collapsed;

        public Visibility BorderVisibility
        {
            get { return borderVisibility; }
            set { SetProperty(ref borderVisibility , value); }
        }

        private string vehicleType;

        public string VehicleType
        {
            get { return vehicleType; }
            set { SetProperty(ref vehicleType, value); }
        }
        private string registrationNO = string.Empty;//"DL8CA2190";

        public string RegistrationNo
        {
            get { return registrationNO; }
            set { SetProperty(ref registrationNO, value); }
        }
        private string slotId = "PMS Exit Details";

        public string SlotId
        {
            get { return slotId; }
            set { SetProperty(ref slotId, value); }
        }
        private DateTime departureTime = DateTime.Now.ToLocalTime();

        public DateTime DepartureTime
        {
            get { return departureTime; }
            set { SetProperty(ref departureTime, value); }
        }
        private string arrivalTime = string.Empty;

        public string ArrivalTime
        {
            get { return arrivalTime; }
            set { SetProperty(ref arrivalTime, value); }
        }
        private string elapsedTime =string.Empty;
        public string ElapsedTime
        {
            get { return elapsedTime; }
            set { SetProperty(ref elapsedTime, value); }
        }
        private double parkingCharges =0;

        public double ParkingCharges
        {
            get { return parkingCharges; }
            set { SetProperty(ref parkingCharges, value); }
        }
        private bool isPaid = false;

        public bool IsPaid
        {
            get { return isPaid; }
            set { SetProperty(ref isPaid, value); }
        }

        public string Error
        {
            get
            {
                return this[string.Empty];
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "RegistrationNo")
                {
                    if (string.IsNullOrEmpty(RegistrationNo))
                    {
                        result = "RegistrationNo cannot be empty";
                        return result;
                    }

                    if (!AppConstant.ValidateInput(RegistrationNo))
                    {
                        result = "Invalid registration number!";
                        return result;
                    }
                }


                return null;
            }
        }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand ResetCommand { get; set; }
        public ViewExitViewModel()
        {
            SearchCommand = new DelegateCommand(GetDetails, CanExecute).ObservesProperty(() => RegistrationNo) ;
            ResetCommand = new DelegateCommand(Reset,CanExecute).ObservesProperty(()=>IsPaid);
        }

        private void GetDetails()
        {
            parkedVehicle_  =PMSService.GetbyId(RegistrationNo);
            if (parkedVehicle_ != null)
            {  
                RegistrationNo = parkedVehicle_.RegistrationId;
                string[] temp = { parkedVehicle_.Floor, parkedVehicle_.SlotId.ToString()};
                SlotId = string.Join("-",temp);                    //parkedVehicle_.SlotId.ToString();
                VehicleType = parkedVehicle_.VehicleType;
                ArrivalTime = Convert.ToDateTime(parkedVehicle_.ArrivalTime).ToString(@"hh\:mm\ tt");
                var timeSpan= (DepartureTime.Subtract(Convert.ToDateTime(parkedVehicle_.ArrivalTime)));
                ElapsedTime = timeSpan.ToString(@"hh\:mm"); // - res.ArrivalTime).ToString();
                if (parkedVehicle_.VehicleType==AppConstant.MyEnum.Fourwheeler.ToString())
                {
                    ParkingCharges = Math.Round((timeSpan.TotalHours * ChargesPerHrs_FourWheeler),2);
                }
                else
                {
                    ParkingCharges = Math.Round((timeSpan.TotalHours * ChargesPerHrs_TwoWheeler),2);
                }
                BorderVisibility = Visibility.Visible;
            }
            else
            {
                MessageBoxResult messageBoxResult= MessageBox.Show("This Vehicle does not exist in the Parking Area!","Sorry!",MessageBoxButton.OK);
                if (messageBoxResult==MessageBoxResult.OK)
                {
                    RegistrationNo = string.Empty;
                }
            }
            
        }

        private bool CanExecute()
        {
            return  !String.IsNullOrEmpty(RegistrationNo) && AppConstant.ValidateInput(RegistrationNo);
        }

        private void Reset()
        {
            IsPaid = false;
            RegistrationNo = string.Empty;
            VehicleType = null;
            SlotId = string.Empty;
            DepartureTime = DateTime.Now.ToLocalTime();
            ElapsedTime = string.Empty;
            ArrivalTime = string.Empty;
            ParkingCharges =0.0;
            if (parkedVehicle_!=null)
            {
                PMSService.Delete(parkedVehicle_);
            }
            BorderVisibility = Visibility.Collapsed;
        }

       
    }
}

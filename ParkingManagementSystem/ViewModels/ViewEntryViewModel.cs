using ParkingManagementSystem.Constant;
using PMSBusinessLayer;
using PMSModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace ParkingManagementSystem.ViewModels
{
    public class ViewEntryViewModel : BindableBase, IDataErrorInfo
    {

        const int Max_Slot_2Wheeler = 205;
        const int Max_Slot_4Wheeler = 100;
        string floor = string.Empty;

        private string title = "PMS Entry Form";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        //private List<AppConstant.MyEnum> vehicleType=new List<AppConstant.MyEnum>();

        //public List<AppConstant.MyEnum> VehicleType
        //{
        //    get { return vehicleType; }
        //    set { SetProperty(ref vehicleType, value); }
        //}

        private string slotID = string.Empty;// "F1-001";

        public string SlotID
        {
            get { return slotID; }
            set { SetProperty(ref slotID, value); }
        }

        private DateTime entryTime = DateTime.Now;

        public DateTime EntryTime
        {
            get { return entryTime; }
            set { SetProperty(ref entryTime, value); }
        }

        private string registrationNo = "DL8CA2190";

        public string RegistrationNo
        {
            get { return registrationNo; }
            set { SetProperty(ref registrationNo, value); }
        }

        public IList<AppConstant.MyEnum> VehicleTypes
        {
            get
            {
                // Will result in a list like {"Tester", "Engineer"}
                return Enum.GetValues(typeof(AppConstant.MyEnum)).Cast<AppConstant.MyEnum>().ToList<AppConstant.MyEnum>();
            }
        }

        private AppConstant.MyEnum vehicleType;
        public AppConstant.MyEnum VehicleType
        {
            get { return vehicleType; }
            set
            {
                SetProperty(ref vehicleType, value);
                //Call made to get the SlotID from Table Vehicle_Parking
                if (vehicleType==AppConstant.MyEnum.Fourwheeler )
                {
                    floor = "F2";
                    GenerateSlotID(floor);
                }
                else if(vehicleType == AppConstant.MyEnum.Twowheeler)
                {
                    floor = "F1";
                    GenerateSlotID(floor);
                }
            }
        }

        public DelegateCommand SaveCommand { get; set; }

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
                if (columnName == "SlotID")
                {
                    if (string.IsNullOrEmpty(SlotID))
                    {
                        result = "SlotId cannot be empty.";
                        return result;
                    }
                }

                if (columnName == "EntryTime")
                {
                    if (string.IsNullOrEmpty(EntryTime.ToString()))
                    {
                        result = "Entry Time cannot be empty";
                        return result;
                    }
                }

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

        public ViewEntryViewModel()
        {
            //vehicleType.Add(AppConstant.MyEnum.TwoWheeler);
            //vehicleType.Add(AppConstant.MyEnum.FourWheeler);
            //vehicleType.Add(AppConstant.MyEnum.Others);
            SaveCommand = new DelegateCommand(Excute, CanExecute).ObservesProperty(() => SlotID).ObservesProperty(() => EntryTime).ObservesProperty(() => RegistrationNo);
        }

        private bool CanExecute()
        {
            return !String.IsNullOrWhiteSpace(SlotID) && !String.IsNullOrWhiteSpace(EntryTime.ToString()) && !String.IsNullOrWhiteSpace(RegistrationNo);
        }

        private void Excute()
        {
            if (AppConstant.ValidateInput(RegistrationNo))
            {
                try
                {
                    var id = SlotID.Split('-').ToList();
                    PMSService.Insert(new Vehicle_Parking()
                    {
                        RegistrationId = RegistrationNo,
                        ArrivalTime = EntryTime.ToLocalTime(),
                        DepartTime = null,
                        SlotId = Convert.ToInt16(id.ElementAtOrDefault(1)),
                        TotalFare = null,
                        VehicleType = VehicleType.ToString(),
                        Floor = id.ElementAt(0).ToString()
                        
                    });
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.Message);
                }
               MessageBoxResult messageBoxResult= MessageBox.Show("Data inserted Successfully!", "Saved",MessageBoxButton.OK);
                if (messageBoxResult==MessageBoxResult.OK)
                {
                    ClearForm();
                }
            }
            else
            {
                MessageBox.Show("Enter valid Registration No!", "Oops");
            }


        }

        private void ClearForm()
        {
            RegistrationNo = string.Empty;
            SlotID = String.Empty;
            EntryTime = DateTime.Now.ToLocalTime();
            VehicleType = AppConstant.MyEnum.Select;
        }

        private void GenerateSlotID(string flr)
        {
            var parkedId = PMSService.GetNextSlotId(flr);
            if (parkedId != null && parkedId.Count>0)
            {
                if (VehicleType == AppConstant.MyEnum.Twowheeler)
                {
                    //var tempValue = parkedId.Split('-').ToList();
                    //tempValue.ElementAtOrDefault(1).FirstOrDefault()
                    //foreach (var item in parkedId)
                    //{
                        if (parkedId.ElementAtOrDefault(0).FirstOrDefault() >= Max_Slot_2Wheeler)
                        {
                            MessageBox.Show("Parking is Full!", "Oops!!");
                        }
                        else
                        {
                            string[] strArray = { parkedId.LastOrDefault().ToString(), parkedId.FirstOrDefault().ToString() };
                            SlotID = string.Join("-", strArray);
                        }
                   // }
                    //if (parkedId>= Max_Slot_2Wheeler)
                    //{
                    //    MessageBox.Show("Parking is Full!", "Oops!!");
                    //}
                    //else
                    //{
                    //    string[] strArray = { tempValue.ElementAtOrDefault(0), (Convert.ToInt32(tempValue.ElementAtOrDefault(1)) + 1).ToString() };
                    //    SlotID = string.Join("-", strArray);
                    //}
                }
                else if (VehicleType == AppConstant.MyEnum.Fourwheeler)
                {
                    //foreach (var item in parkedId)
                    //{
                    //    if (item.FirstOrDefault() >= Max_Slot_4Wheeler)
                    //    {
                    //        MessageBox.Show("Parking is Full!", "Oops!!");
                    //    }
                    //    else
                    //    {
                    //        string[] strArray = { item.LastOrDefault().ToString(), item.FirstOrDefault().ToString() };
                    //        SlotID = string.Join("-", strArray);
                    //    }
                    //}
                    if (parkedId.ElementAtOrDefault(1).FirstOrDefault() >= Max_Slot_4Wheeler)
                    {
                        MessageBox.Show("Parking is Full!", "Oops!!");
                    }
                    else
                    {
                        string[] strArray = { parkedId.LastOrDefault().ToString(), parkedId.FirstOrDefault().ToString() };
                        SlotID = string.Join("-", strArray);
                    }
                    //var tempValue = parkedId.Split('-').ToList();
                    //if (tempValue.ElementAtOrDefault(1).FirstOrDefault() >= Max_Slot_4Wheeler)
                    //{
                    //    MessageBox.Show("Parking is Full!", "Oops!!");
                    //}
                    //else
                    //{
                    //    string[] strArray = { tempValue.ElementAtOrDefault(0), (Convert.ToInt32(tempValue.ElementAtOrDefault(1)) + 1).ToString() };
                    //    SlotID = string.Join("-", strArray);
                    //}
                }
            }
            else
            {
                if (VehicleType == AppConstant.MyEnum.Twowheeler)
                {
                    SlotID = "F1-1";
                }
                else if (VehicleType == AppConstant.MyEnum.Fourwheeler)
                {
                    SlotID = "F2-1";
                }
            }

        }
        //private bool ValidateInput()
        //{
        //    bool IsValid = false;
        //    if (!String.IsNullOrEmpty(SlotID) )
        //    {
        //        IsValid = true;
        //    }
        //    return IsValid;
        //}

        //private bool ValidateInput()
        //{
        //    bool IsValid = false;
        //    String reg = @"^[a-z]{2}[a-z0-9]{1,2}[a-z]{1,2}[0-9]{4}$";//@"^[a-z]{2}[0-9][a-z]{1,2}[a-z]{1,2}[0-9]{4}$";
        //    Regex regexVeh = new Regex(reg, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //    Match match = regexVeh.Match(RegistrationNo.Trim());
        //    if (match.Success)
        //    {
        //        IsValid = true;
        //    }

        //    return IsValid;

        //}
    }
}

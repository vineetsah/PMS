using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDataLayer;
using PMSModel;

namespace PMSBusinessLayer
{
    public static class PMSService
    {
        static IPMSRepository repository;

        static PMSService()
        {
            repository = new PMSRepository();
        }
        public static List<Vehicle_Parking> GetAll()
        {
            return repository.GetAllVehicles();
        }

        public static Vehicle_Parking GetbyId(string id)
        {
            return repository.GetById(id);
        }

        public static Vehicle_Parking Insert(Vehicle_Parking obj)
        {
            return repository.Insert(obj);
        }

        public static void Update(Vehicle_Parking obj)
        {
            repository.Update(obj);
        }
        public static void Delete(Vehicle_Parking obj)
        {
            repository.Delete(obj);
        }

        public static IList<string> GetNextSlotId(string str)
        {
            return repository.GetNextSlotId(str);
        }
    }
}

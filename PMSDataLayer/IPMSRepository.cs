using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;

namespace PMSDataLayer
{
   public interface IPMSRepository
    {
        List<Vehicle_Parking> GetAllVehicles();
        Vehicle_Parking GetById(string id);
        Vehicle_Parking Insert(Vehicle_Parking obj);
        void Update(Vehicle_Parking obj);
        void Delete(Vehicle_Parking obj);
        IList<string> GetNextSlotId(string str);
    }
}

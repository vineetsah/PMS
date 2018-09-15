using PMSModel;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PMSDataLayer
{
    public class PMSRepository : IPMSRepository
    {
        public void Delete(Vehicle_Parking obj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                db.Vehicle_Parking.Attach(obj);
                db.Vehicle_Parking.Remove(obj);
                db.SaveChanges();
            }
        }

        public List<Vehicle_Parking> GetAllVehicles()
        {
            using (PMSEntities db = new PMSEntities())
            {
                return db.Vehicle_Parking.ToList();
            }
        }

        public Vehicle_Parking GetById(string id)
        {
            using (PMSEntities db = new PMSEntities())
            {
                return db.Vehicle_Parking.Find(id);
            }
        }

        public Vehicle_Parking Insert(Vehicle_Parking obj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                db.Vehicle_Parking.Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        public void Update(Vehicle_Parking obj)
        {
            using (PMSEntities db = new PMSEntities())
            {
                db.Vehicle_Parking.Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IList<string> GetNextSlotId(string floor)
        {
            try
            {
                using (PMSEntities db = new PMSEntities())
                {
                    //var result = db.Vehicle_Parking.Where(v => v.VehicleType == vehicletype).Select(p => p.SlotId).DefaultIfEmpty().Max();
                    //return result.ToString();

                    //var result = (from T1 in db.Vehicle_Parking
                    //             join T2 in db.Vehicle_Parking on T1.SlotId + 1 equals T2.SlotId  // && T1.Floor equals T2.Floor 
                    //             into T3
                    //             from T4 in (from T)
                    //                        .DefaultIfEmpty()                              //in T3.DefaultIfEmpty()
                    //                        where T4.SlotId is null && T4.Floor="F1"
                    //                        orderby T1.SlotId
                    //             select new { T1.SlotId  , T1.Floor }).ToList();

                    IList<string> result = new List<string>();
                    var q = (from v in db.Vehicle_Parking
                             join w in db.Vehicle_Parking
                             on new { a = v.SlotId + 1, v.Floor } equals new { a = w.SlotId, w.Floor }
                             into x
                             from x1 in x
                              .DefaultIfEmpty()
                             orderby x1.SlotId
                             select new
                             {
                                 NextId = v.SlotId + 1,
                                 v.Floor,
                             }).Where(s => s.Floor ==floor).Take(1);

                    foreach (var item in q)
                    {
                        result.Add(item.NextId.ToString());
                        result.Add(item.Floor.ToString());
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

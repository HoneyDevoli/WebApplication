using System;
using System.Collections.Generic;
using System.Linq;
using NoNameApp.Entities;
using System.Data.Entity;
using NoNameApp.DalContracts;

namespace NoNameApp.Repository
{
    public class SerialRepository:IRepository<Serial>
    {
        private DAL.EF.AppContext db;

        public SerialRepository(DAL.EF.AppContext context)
        {
            this.db = context;
        }

        public IEnumerable<Serial> GetAll()
        {
            return db.Serials;
        }

        public Serial Get(int id)
        {
            return db.Serials.Find(id);
        }

        public void Create(Serial serial)
        {
            db.Serials.Add(serial);
        }

        public void Update(Serial serial)
        {
            db.Entry(serial).State = EntityState.Modified;
        }

        public IEnumerable<Serial> Find(Func<Serial, Boolean> predicate)
        {
            return db.Serials.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Serial serial = db.Serials.Find(id);
            if (serial != null)
                db.Serials.Remove(serial);
        }
    }
}

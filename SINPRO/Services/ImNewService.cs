using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINPRO.Services
{
    public interface ImNewService
    {
        IQueryable<mNew> GetAll();
        mNew GetByID(int id);
        mNew Insert(mNew item);
        mNew Update(mNew item);
        mNew Delete(int id);
    }
    public class mNewService : ImNewService
    {
        private readonly SINContext _db;
        public mNewService(SINContext db)
        {
            _db = db;
        }
        public mNew Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public IQueryable<mNew> GetAll()
        {
            return _db.mNew.AsQueryable();
        }

        public mNew GetByID(int id)
        {
            return GetAll().FirstOrDefault(p => p.id == id);
        }

        public mNew Insert(mNew item)
        {
            item.inserted = DateTime.Now;
            _db.mNew.Add(item);
            _db.SaveChanges();
            return GetByID(GetAll().Max(p => p.id));
        }

        public mNew Update(mNew item)
        {
            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;
            _db.Update(item);
            return item;
        }
    }
}

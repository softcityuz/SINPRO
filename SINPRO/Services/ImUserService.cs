using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINPRO.Services
{
    public interface ImUserService
    {
        IQueryable<mUser> GetAll();
        mUser GetByID(int id);
        mUser Insert(mUser item);
        mUser Update(mUser item);
        mUser Delete(int id);
    }
    public class mUserService : ImUserService
    {
        private readonly SINContext _db;

        public mUserService(SINContext db)
        {
            _db = db;
        }
        public mUser Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public IQueryable<mUser> GetAll()
        {
            return _db.mUser.AsQueryable();
        }

        public mUser GetByID(int id)
        {
            return GetAll().FirstOrDefault(p => p.id == id);
        }

        public mUser Insert(mUser item)
        {
            item.inserted = DateTime.Now;
            _db.mUser.Add(item);
            _db.SaveChanges();
            return GetByID(GetAll().Max(p => p.id));
        }

        public mUser Update(mUser item)
        {

            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;
            _db.Update(item);
            return item;
        }
    }
}

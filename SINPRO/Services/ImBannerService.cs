using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINPRO.Services
{
    public interface ImBannerService
    {
        IQueryable<mBanner> GetAll();
        mBanner GetByID(int id);
        mBanner Insert(mBanner item);
        mBanner Update(mBanner item);
        mBanner Delete(int id);
    }
    public class mBannerService : ImBannerService
    {
        private readonly SINContext _db;

        public mBannerService(SINContext db)
        {
            _db = db;
        }
        public mBanner Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public IQueryable<mBanner> GetAll()
        {
            return _db.mBanner.AsQueryable();
        }

        public mBanner GetByID(int id)
        {
            return GetAll().FirstOrDefault(p => p.id == id);
        }

        public mBanner Insert(mBanner item)
        {
            item.inserted = DateTime.Now;
            _db.mBanner.Add(item);
            _db.SaveChanges();
            return GetByID(GetAll().Max(p => p.id));
        }

        public mBanner Update(mBanner item)
        {
            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;
            _db.Update(item);
            return item;
        }
    }
}

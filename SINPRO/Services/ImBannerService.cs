using Microsoft.EntityFrameworkCore;
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
        IAsyncEnumerable<mBanner> GetAllAsync();
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

        public IAsyncEnumerable<mBanner> GetAllAsync()
        {
            return _db.mBanner.AsAsyncEnumerable();
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
            SINContextFactory contextFactory = new SINContextFactory();
            var db = contextFactory.CreateDbContext(default);
            item.inserted = GetByID(item.id).inserted;
            item.photo = item.photo == null || item.photo == "" ? GetByID(item.id).photo : item.photo;
            item.updated = DateTime.Now;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }
    }
}

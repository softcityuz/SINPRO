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
    public interface ImMatchService
    {
        IQueryable<mMatch> GetAll();
        mMatch GetByID(int id);
        mMatch Insert(mMatch item);
        mMatch Update(mMatch item);
        mMatch Delete(int id);
    }
    public class mMatchService : ImMatchService
    {
        private readonly SINContext _db;
        public mMatchService(SINContext db)
        {
            _db = db;
        }
        public mMatch Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public IQueryable<mMatch> GetAll()
        {
            return _db.mMatch.AsQueryable();
        }

        public mMatch GetByID(int id)
        {
            return GetAll().FirstOrDefault(p => p.id == id);
        }

        public mMatch Insert(mMatch item)
        {
            item.inserted = DateTime.Now;
            _db.mMatch.Add(item);
            _db.SaveChanges();
            return GetByID(GetAll().Max(p => p.id));
        }

        public mMatch Update(mMatch item)
        {
            SINContextFactory contextFactory = new SINContextFactory();
            var db = contextFactory.CreateDbContext(default);
            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;
            item.firstTimePhoto = item.firstTimePhoto == null || item.firstTimePhoto == "" ? GetByID(item.id).firstTimePhoto : item.firstTimePhoto;
            item.secondTimePhoto = item.secondTimePhoto == null || item.secondTimePhoto == "" ? GetByID(item.id).secondTimePhoto : item.secondTimePhoto;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }
    }
}

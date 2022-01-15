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
    public interface ImRoleService
    {
        IQueryable<mRole> GetAll();
        mRole GetByID(int id);
        mRole Insert(mRole item);
        mRole Update(mRole item);
        mRole Delete(int id);
    }
    public class mRoleService : ImRoleService
    {
        private readonly SINContext _db;

        public mRoleService(SINContext db)
        {
            _db = db;
        }
        public mRole Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public IQueryable<mRole> GetAll()
        {
            return _db.mRole.AsQueryable();
        }

        public mRole GetByID(int id)
        {
            return GetAll().FirstOrDefault(p => p.id == id);
        }

        public mRole Insert(mRole item)
        {
            item.inserted = DateTime.Now;
            _db.mRole.Add(item);
            _db.SaveChanges();
            return GetByID(GetAll().Max(p => p.id));
        }

        public mRole Update(mRole item)
        {
            SINContextFactory contextFactory = new SINContextFactory();
            var db = contextFactory.CreateDbContext(default);
            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }
    }
}

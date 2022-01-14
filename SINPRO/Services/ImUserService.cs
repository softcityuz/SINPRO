using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        mUser GetBySignIn(string email, string password);
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

        public mUser GetBySignIn(string email, string password)
        {
            var res = GetAll().Where(_ => _.email == email).FirstOrDefault();

            if (res != null && ValidatePasswordHash(password, res.password))
            {
                return res;
            }
            return null;
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
        private bool ValidatePasswordHash(string password, string dbPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(dbPassword);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}

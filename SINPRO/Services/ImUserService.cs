using Microsoft.EntityFrameworkCore;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using SINPRO.Logic;
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
        string findEmail(string login);
    }
    public class mUserService : ImUserService
    {
        private readonly SINContext _db;
        private readonly IAuthLogic _authLogic;

        public mUserService(SINContext db, IAuthLogic authLogic)
        {
            _db = db;
            _authLogic = authLogic;
        }
        public mUser Delete(int id)
        {
            var _delResult = GetByID(id);
            _db.Remove(_delResult);
            _db.SaveChanges();
            return _delResult;
        }

        public string findEmail(string login)
        {
            var r = GetAll().Where(p =>login!=null && p.email.ToLower() == login.ToLower());
            if (r.Count() > 0)
            {
                return "Exist";
            }
            return "";
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
            _authLogic.Register(item);
            return GetByID(GetAll().Max(p => p.id));
        }

        public mUser Update(mUser item)
        {

            SINContextFactory contextFactory = new SINContextFactory();
            var db = contextFactory.CreateDbContext(default);
            item.inserted = GetByID(item.id).inserted;
            item.updated = DateTime.Now;

            item.password = item.password == ""||item.password == null ? GetByID(item.id).password : PasswordHash(item.password);
            item.email = GetByID(item.id).email;
            item.roleId = GetByID(item.id).roleId;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }
        private bool ValidatePasswordHash(string password, string dbPassword)
        {
            if (password==null)
            {
                return false;
            }
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
        private string PasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}

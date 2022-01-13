using Microsoft.EntityFrameworkCore;
using SINPRO.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity
{
    public class SINContext : DbContext
    {
        public SINContext(DbContextOptions<SINContext> options)
            : base(options)
        {
        }
        //base table
        public DbSet<mBanner> mBanner { get; set; }
        public DbSet<mMatch> mMatch { get; set; }
        public DbSet<mNew> mNew { get; set; }
        public DbSet<mRole> mRole { get; set; }
        public DbSet<mUser> mUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mRole>().HasData(new mRole[] {
                new mRole{id=1,parentId=1,resourceName="admin",status=true,inserted=DateTime.Now,updated=DateTime.Now},
                new mRole{id=2,parentId=2,resourceName="user",status=true,inserted=DateTime.Now,updated=DateTime.Now}
            });
            modelBuilder.Entity<mUser>().HasData(new mUser[] {
                new mUser{id=1,email="admin@gmail.com",password="DDWjBLfJgK1/V+Owzhs/E9yvyP/YGaFnsFpbECWHh/cuafCO",status=1,roleId=1,inserted=DateTime.Now,updated=DateTime.Now}
            });
        }
    }
}

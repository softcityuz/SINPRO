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

        }
    }
}

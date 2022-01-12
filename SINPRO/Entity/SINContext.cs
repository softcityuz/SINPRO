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
        public DbSet<mBanners> mBanners { get; set; }
        public DbSet<mMatches> mMatches { get; set; }
        public DbSet<mNews> mNews { get; set; }
        public DbSet<mRoles> mRoles { get; set; }
        public DbSet<mUsers> mUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

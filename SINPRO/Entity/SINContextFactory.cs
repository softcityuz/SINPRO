using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Entity
{
    public class SINContextFactory
    {
        public SINContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SINContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new SINContext(builder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.Common.Configuration;
using TeamsIntegration.Common.Utilities;
using TeamsIntegration.Data.models;


namespace TeamsIntegration.Data
{
   public class TeamDBContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public TeamDBContext(DbContextOptions<TeamDBContext> options, IConfiguration configuration) :base(options)
        {
            Configuration = configuration;
        }
        public DbSet<TeamAppInstallation> TeamAppsInstallation { get; set; }
        public DbSet<UnMappedClass> UnMappedClasses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamAppInstallation>().HasKey(o => new {o.TeamId,o.AppId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(@"Server=tcp:itworxdbserver.database.windows.net,1433;Initial Catalog=QBDB;Persist Security Info=False;User ID=SystemAdmin;Password=ITWorx@cloud;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                optionsBuilder.UseSqlServer(AuthenticationConfig.Get("ConnectionStrings", "DefaultConnection"));


            }
        }
   }


    
}

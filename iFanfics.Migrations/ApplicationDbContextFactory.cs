using iFanfics.DAL.EF;
using iFanfics.Migrations.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace iFanfics.Migrations
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext> {
        public ApplicationContext CreateDbContext(string[] args) {
            var configuration = new ConfigurationHelper().Configuration;

            System.IO.File.WriteAllText("D:/contextfactory.txt", configuration.GetConnectionString("ApplicationContext"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("iFanfics.Migrations"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}

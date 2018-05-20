using iFanfics.DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace iFanfics.DAL.EF {
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext> {
        public ApplicationContext CreateDbContext(string[] args) {
            var configuration = new ConfigurationHelper().Configuration;

            System.IO.File.WriteAllText("D:/contextfactory.txt", configuration.GetConnectionString("ApplicationContext"));

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("iFanfics.DAL"));

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
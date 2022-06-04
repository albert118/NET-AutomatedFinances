using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomatedFinances.Infrastructure
{
    public sealed class IridiumDbContext : DbContext
    {
        public IridiumDbContext(DbContextOptions<IridiumDbContext> opts) : base(opts) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        {
            // configuration for a JSON settings file will not work without this package installed
            // Microsoft.Extensions.Configuration.Json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            builder.UseSqlServer(configuration.GetConnectionString("IridiumDbConnection"));
        }
        
        public DbSet<PaymentMethod>? PaymetMethods { get; set; }
    }
    
}
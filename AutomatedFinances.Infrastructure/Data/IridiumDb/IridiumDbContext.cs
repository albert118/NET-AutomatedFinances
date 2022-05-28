using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Infrastructure.Data.IridiumDb
{
    public class IridiumDbContext : DbContext
    {
        public IridiumDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<PaymentMethod>? PaymetMethods { get; set; }
    }
}
using AutomatedFinances.Application.Interfaces.Expenditures;
using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Infrastructure.Data.ExpendituresDb
{
    internal class ExpenditureWriteDbContext : AsyncDbContext, IExpenditureWriteDbContext
    {
        public ExpenditureWriteDbContext(
            DbContextOptions<ExpenditureWriteDbContext> options) 
            : this((DbContextOptions)options) { }

        protected ExpenditureWriteDbContext(
            DbContextOptions options) 
            : base(options) { }

        public DbSet<Business> Businesses => Set<Business>();

        public DbSet<Expenditure> Expenditures => Set<Expenditure>();

        public DbSet<Cost> Costs => Set<Cost>();

        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var me = typeof(ExpenditureWriteDbContext);
            var mynamespace = me.Namespace;

            modelBuilder.ApplyConfigurationsFromAssembly(me.Assembly, type =>
                type.Namespace?.StartsWith(mynamespace) ?? false);
        }
    }
}

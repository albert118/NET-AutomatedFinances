using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutomatedFinances.Infrastructure.Data.IridiumDb
{
    internal class IridiumWriteDbContext : AsyncDbContext, IIridiumWriteDbContext
    {
        protected IridiumWriteDbContext(DbContextOptions<IridiumReadDbContext> opts) : base(opts) { }
        protected IridiumWriteDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<PaymentMethod>? PaymentMethods { get; set; }

        /// <summary>
        /// Enable custom model configuration by reflection on the assembly. The context namespace
        /// is determined, then get the model type in the same namespace.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var contextReflection = typeof(IridiumWriteDbContext);
            var contextNamespace = contextReflection.Namespace;

            if (contextNamespace == null) {
                throw new InvalidOperationException(
                    $"Couldn't determine the namespace of {nameof(IridiumWriteDbContext)}, please validate this context and it's data sets are correctly namespaced");
            }

            builder.ApplyConfigurationsFromAssembly(
                contextReflection.Assembly,
                type => type.Namespace?.StartsWith(contextNamespace) ?? false);
        }
    }
}
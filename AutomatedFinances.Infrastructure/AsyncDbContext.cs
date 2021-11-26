using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure
{
    internal abstract class AsyncDbContext : DbContext
    {
        protected AsyncDbContext(DbContextOptions options) : base(options) { }

        public override int SaveChanges(bool _) => SaveChanges();

        public override int SaveChanges() => throw new NotSupportedException("Async is preffered over sync calls.");

        public override Task<int> SaveChangesAsync(CancellationToken ct = default) => SaveChangesAsync(true, ct);

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
        {
            if (ct == default)
            {
                throw new InvalidOperationException("A real cancellation token is need to call SaveChangesAsync() "
                    + "so that uneeded or cancelled operations can be terminated mid-save.");
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, ct);

        }
    }
}

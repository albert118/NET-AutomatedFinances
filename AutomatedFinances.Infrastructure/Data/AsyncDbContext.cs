using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedFinances.Infrastructure.Data
{
    internal abstract class AsyncDbContext : DbContext
    {
        protected AsyncDbContext(DbContextOptions options) : base(options) { }

        public override int SaveChanges(bool _) => SaveChanges();

        public override int SaveChanges() => throw new NotSupportedException($"Please use {nameof(SaveChangesAsync)}()."
            + "Async is da bossssss. Don't be syncro!");

        public override Task<int> SaveChangesAsync(CancellationToken ct = default) => SaveChangesAsync(true, ct);


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
        {
            if (ct == default)
            {
                throw new InvalidOperationException("Please pass a real cancellation token to SaveChangesAsync() so "
                    + "that any uneeded queries can be stopped early! Otherwise async is not being used correctly.";
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, ct);
        }
    }
}

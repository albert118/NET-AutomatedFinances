using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Application.Interfaces;

public interface ITradingTransactionReadDbContext
{
    DbSet<GenericTransaction> GenericTransactions { get; }
}

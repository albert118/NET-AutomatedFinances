using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Application.Interfaces;

public interface ITransactionReadDbContext
{
    DbSet<FinancialTransactionRecord> FinancialTransactionRecords { get; }
}

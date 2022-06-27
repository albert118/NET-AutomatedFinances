using AutomatedFinances.Application.Interfaces;
using AutomatedFinances.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedFinances.Application;

public class ControllerDemo
{
    private readonly ITradingTransactionWriteDbContext _transactionWriteDbContext;

    public ControllerDemo(ITradingTransactionWriteDbContext transactionWriteDbContext)
    {
        _transactionWriteDbContext = transactionWriteDbContext;
    }

    public List<GenericTransaction> GetExistingTransactions()
    {
        return _transactionWriteDbContext.GenericTransactions.ToList();
    }

    public void AddNewTransaction()
    {
        _transactionWriteDbContext.GenericTransactions.Add(new() {TrackingId = Guid.NewGuid()});
    }
}

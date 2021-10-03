using AutomatedFinances.BusinessCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Application.Interfaces.Expenditures
{
    public interface IExpenditureReadDbContext
    {

        DbSet<Business> Businesses {get; }

        DbSet<Expenditure> Expenditures { get; }

        DbSet<Cost> Costs{ get; }

        DbSet<PaymentMethod> PaymentMethods { get; }

        DbSet<Note> Notes { get; }
    }
}

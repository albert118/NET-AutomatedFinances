using AutomatedFinances.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomatedFinances.Application.Interfaces
{
    public interface IIridiumReadDbContext
    {
        DbSet<PaymentMethod> PaymentMethods { get; }
    }
}
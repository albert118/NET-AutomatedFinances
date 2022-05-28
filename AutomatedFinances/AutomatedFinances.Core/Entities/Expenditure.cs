using AutomatedFinances.Core.Enumerations;
using System;

namespace AutomatedFinances.Core.Entities
{
    public class Expenditure
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }

        public CurrencyCode CurrencyCode { get; set; } = CurrencyCode.None;

        public string Description { get; set; }

        public string Message { get; set; }
    }
}
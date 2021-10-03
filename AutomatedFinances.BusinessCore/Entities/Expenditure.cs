using AutomatedFinances.BusinessCore.Enumerations;
using System;

namespace AutomatedFinances.BusinessCore.Entities
{
    public class Expenditure
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }

        public CurrencyCode CurrencyCode { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }
    }
}

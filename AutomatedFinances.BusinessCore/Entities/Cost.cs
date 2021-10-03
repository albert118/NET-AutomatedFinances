using AutomatedFinances.BusinessCore.Enumerations;
using System;

namespace AutomatedFinances.BusinessCore.Entities
{
    public class Cost
    {
        public Guid Id { get; set; }

        public Category Category { get; set; } = Category.None;

        public Business Business { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public Note Note { get; set; }
    }
}

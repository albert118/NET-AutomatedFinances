using AutomatedFinances.BusinessCore.Enumerations;
using System;
using System.Collections.Generic;

namespace AutomatedFinances.BusinessCore.Entities
{
    public class Cost
    {
        public Guid Id { get; set; }

        public Category Category { get; set; } = Category.None;

        public Business Business { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<Note> Note { get; set; } = new HashSet<Note>();
    }
}

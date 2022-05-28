using System;
using System.Collections.Generic;

namespace AutomatedFinances.Core.Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public ICollection<Cost> Cost { get; set; } = new HashSet<Cost>();
    }
}
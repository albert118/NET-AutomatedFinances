using System;

namespace AutomatedFinances.BusinessCore.Entities
{
    public class Business
    {
        public Guid Id { get; set; }

        public string ACN { get; set; }

        public int ABN { get; set; }

        public string BusinessName { get; set; }
    }
}

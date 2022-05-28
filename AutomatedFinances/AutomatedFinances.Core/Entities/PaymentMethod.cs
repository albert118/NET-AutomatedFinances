using AutomatedFinances.Core.Enumerations;

namespace AutomatedFinances.Core.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        // biz?
        public string Source { get; set; }

        public PaymentType Method { get; set; }
    }
}
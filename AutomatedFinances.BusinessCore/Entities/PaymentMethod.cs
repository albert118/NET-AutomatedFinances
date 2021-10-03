namespace AutomatedFinances.BusinessCore.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        public string Source { get; set; }

        public string MethodType { get; set; }
    }
}

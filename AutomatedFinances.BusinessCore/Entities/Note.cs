using System.Collections.Generic;

namespace AutomatedFinances.BusinessCore.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public ICollection<Cost> Costs { get; set; } = new HashSet<Cost>();
    }
}

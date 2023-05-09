using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Discount:Monitoring
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}

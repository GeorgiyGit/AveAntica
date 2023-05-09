using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Domain.Entities.Language;

namespace Server.Domain.Entities
{
    public class OrderType
    {
        public int Id { get; set; }

        public ICollection<OrderTypeTranslatedText> Names { get; set; } = new HashSet<OrderTypeTranslatedText>();
        public ICollection<OrderTypeTranslatedText> Descriptions { get; set; } = new HashSet<OrderTypeTranslatedText>();

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ICollection<Discount> Discounts { get; set; } = new HashSet<Discount>();

        public int TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public OrderType Status { get; set; }
        public int StatusId { get; set; }
    }
}

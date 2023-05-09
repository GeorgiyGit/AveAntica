using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class LocalChat
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public string CustomerId { get; set; }

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();

    }
}

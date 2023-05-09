using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Image:Monitoring
    {
        public int Id { get; set; }
        public string Title { get; set; } //name of image
        public string Name { get; set; } //saved name + .png

        public Category? Category { get; set; }
        public int? CategoryId { get; set; }

        public Product? Product { get; set; }
        public int? ProductId { get; set; }

        public Customer? Customer { get; set; }
        public string? CustomerId { get; set; }

        public Tag? Tag { get; set; }
        public int? TagId { get; set; }
    }
}

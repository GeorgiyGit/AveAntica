using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Domain.Entities
{
    public class Customer : IdentityUser, IMonitoring
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public bool IsEdited { get; set; }
        public DateTime LastEditTime { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime LastDeleteTime { get; set; }

        public Image? Avatar { get; set; }

        public Basket Basket { get; set; }

        public GlobalChat GlobalChat { get; set; }

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public ICollection<Product> LikedProducts { get; set; } = new HashSet<Product>();
    }
}
